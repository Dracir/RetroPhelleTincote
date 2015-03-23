import os
from bpy import *
from tools import *

REMESH_PRECISION_THRESHOLD = 10      # How many points befor using more precision (should stay between 5 and 50)

def main():
    for curve in get_curves():
        curve.select = True
        context.scene.objects.active = curve
        solidify_thickness = get_solidify_thickness(curve)
        remesh_precision = get_remesh_precision(curve)

        rotate_curve(curve)
        convert_to_mesh()
        fill_mesh(curve)
        solidify(solidify_thickness)
        remesh(remesh_precision)
        triangulate()
        add_material(curve)
        add_texture(curve)
        unwrap()

def get_curves():
    curves = []
    blender_directory = os.path.dirname(data.filepath)

    for (_, _, files) in os.walk(os.path.dirname(data.filepath)):
        for file in files:
            svgfile = os.path.splitext(file)[0]
            svgext = os.path.splitext(file)[1]

            if svgext == ".svg":
                curves.append(get_curve(svgfile))
    
    return curves

def get_curve(filename):
    if filename in data.objects.keys():
        object = data.objects[filename]
        object.select = True
        location = object.location
        print(location, object.location)
        ops.object.delete()
        
    path = os.path.dirname(data.filepath) + "\\" + filename + ".svg"
    
    ops.import_curve.svg('EXEC_SCREEN', filepath=path, filter_glob="*.svg")
    curve = get_created(data.objects, "Curve")
    curve.name = filename
    
    return curve
    
def rotate_curve(curve):
    ops.object.select_all(action='DESELECT')
    curve.select = True
    ops.transform.rotate(value=1.5708, axis=(1, 0, 0), constraint_axis=(True, False, False), constraint_orientation='GLOBAL', mirror=False, proportional='DISABLED', proportional_edit_falloff='SMOOTH', proportional_size=1)

def convert_to_mesh():
    ops.object.convert(target='MESH')
    ops.object.transform_apply(location=True, rotation=True, scale=True)

def fill_mesh(curve):
    set_edit_mode(True)
    ops.mesh.select_all(action='SELECT')
    ops.mesh.edge_face_add()
    set_edit_mode(False)

def get_solidify_thickness(curve):
    thickness = 5
    
    if "Head" in curve.name:
        thickness = 6
        
    if "Cheek" in curve.name:
        thickness = 8
        
    if "Mouth" in curve.name:
        thickness = 7
        
    if "Tongue" in curve.name:
        thickness = 12
        
    if "Eye" in curve.name:
        thickness = 10
        
    if "Pupil" in curve.name:
        thickness = 11
    
    if "Brow" in curve.name:
        thickness = 9
        
    if "Arm" in curve.name:
        thickness = 3
        
    if "Hand" in curve.name:
        thickness = 4
        
    if "Leg" in curve.name:
        thickness = 1
        
    if "Foot" in curve.name:
        thickness = 3
    
    return thickness

def solidify(thickness):
    ops.object.modifier_add(type='SOLIDIFY')
    
    modifier = context.object.modifiers["Solidify"]
    modifier.show_viewport = False
    modifier.thickness = thickness * 0.00005
    modifier.offset = 0
    
    ops.object.modifier_apply(apply_as='DATA', modifier="Solidify")

def get_remesh_precision(curve):
    curve_points = len(curve.data.splines[0].bezier_points)
    precision = 2
    
    if curve_points > REMESH_PRECISION_THRESHOLD:
        precision = 3
    
    if curve_points > REMESH_PRECISION_THRESHOLD * 2:
        precision = 4
        
    return precision

def remesh(precision):
    ops.object.modifier_add(type='REMESH')
    
    modifier = context.object.modifiers["Remesh"]
    modifier.show_viewport = False
    modifier.octree_depth = precision
    
    ops.object.modifier_apply(apply_as='DATA', modifier="Remesh")
    
def triangulate():
    ops.object.modifier_add(type='TRIANGULATE')
    ops.object.modifier_apply(apply_as='DATA', modifier="Triangulate")

def add_material(curve):
    material = None
    if curve.name in data.materials.keys():
        material = data.materials[curve.name]
    else:
        ops.material.new()
        material = get_created_material()
    
    material.name = curve.name 
    material.use_shadeless = True
    material.use_transparency = True
    material.alpha = 0
    
    curve.active_material = material

def add_texture(curve):
    texture = None
    if curve.name in data.textures.keys():
        texture = data.textures[curve.name]
    else:
        ops.texture.new()
        texture = get_created_texture()
    
    ops.image.open(filepath = "//" + curve.name + "Texture.png", files = [{"name" : curve.name + "Texture.png", "name" : curve.name + "Texture.png"}], relative_path = True)
    
    material = data.materials[curve.name]
    material.active_texture = texture
    material.texture_slots[0].use_map_alpha = True
    
    texture.name = curve.name
    texture.image = data.images[curve.name + "Texture.png"]
    texture.use_preview_alpha = True
    texture.extension = 'CLIP'

def unwrap():
    for window in context.window_manager.windows:
        screen = window.screen
        for area in screen.areas:
            if area.type == 'VIEW_3D':
                for space in area.spaces:
                    if space.type == 'VIEW_3D':
                        for region in area.regions:
                            if region.type == 'WINDOW':
                                set_edit_mode(True)
                                ops.mesh.select_all(action='SELECT')
                                override = {'window': window, 'screen': screen, 'area': area, 'region': region, 'edit_object': context.edit_object, 'scene': context.scene, 'blend_data': context.blend_data}
                                ops.uv.project_from_view(override, orthographic=True, correct_aspect=True, clip_to_bounds=False, scale_to_bounds=True)
                                set_edit_mode(False)
    
    
main()
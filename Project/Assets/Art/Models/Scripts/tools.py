from bpy import *

def create_plane(location = (0, 0, 0), rotation = (0, 0, 0), layer = 0):
    ops.mesh.primitive_plane_add(location = location, rotation = rotation, layers = get_layer(layer))
    return get_created_plane()

def get_created(collection, type):
    name = ""    
    
    for key in collection.keys():
        if key.startswith(type):
            name = key
            
    return collection[name]

def get_created_plane():
    return get_created(data.objects, "Plane")

def get_created_material():
    return get_created(data.materials, "Material")

def get_created_texture():
    return get_created(data.textures, "Texture")

def get_layer(index):
    layer = []
    
    for i in range(20):
        if (i == index):
            layer.append(True)
        else:
            layer.append(False)
    
    return tuple(layer)

def set_edit_mode(state):
    if context.mode in ('EDIT_MESH', 'EDIT_CURVE'):
        if not state:
            ops.object.editmode_toggle()
    elif state:
        ops.object.editmode_toggle()
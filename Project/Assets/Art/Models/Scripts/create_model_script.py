import os
from bpy import *
from tools import *

def main():
    grouped_vertices = get_grouped_vertices()
    file = write_header()
    write_body(file, grouped_vertices)
    write_footer(file)
    

def get_grouped_vertices():
    armature = data.objects["Armature"]
    grouped_vertices = {}

    for child in armature.children:
        vertices = child.data.vertices
        groups = child.vertex_groups
        
        for vertex in vertices:
            for group in vertex.groups:
                group_name = groups[group.group].name
                grouped_vertices.setdefault(group_name, []).append(vertex)
    
    return grouped_vertices

def write_header():
    current_path = os.path.dirname(data.filepath)
    model_name = os.path.split(current_path)[1]
    file = open(current_path + "\\" + os.path.split(current_path)[1] + "Model.cs", "w")
    
    header = [
        'using UnityEngine;',
        'using System.Collections;',
        'using System.Collections.Generic;',
        'using Magicolo.GraphicsTools;',
        '',
        'namespace Magicolo {',
        '    [AddComponentMenu("Magicolo/Models/' + model_name + '")]',
        '    public class ' +  model_name + 'Model : Model {',
        '    ',
        '        public override Dictionary<string, Vector2[]> BoneNameVerticesDict {',
        '            get {',
        '                return new Dictionary<string, Vector2[]> {'
    ]
    
    file.write("\n".join(header) + "\n")
    
    return file
   
def write_body(file, grouped_vertices):
    body = []
    
    for group_name, mesh_vertices in sorted(grouped_vertices.items()):
        group = '                    { "' + group_name + '", new []{ '
        vertices = []
        
        for vertex in mesh_vertices:
            vertices.append("new Vector2(" + str(vertex.co.x) + "F, " + str(vertex.co.z) + "F)") 
        
        group += ", ".join(vertices) + " }}"
        body.append(group)
    
    file.write(",\n".join(body) + "\n")

def write_footer(file):
    footer = [
        '                };',
        '            }',
        '        }',
        '    }',
        '}'
    ]
    
    file.write("\n".join(footer))
    file.close()
     
     
main()
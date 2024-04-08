import random
from perlin_noise import PerlinNoise

class Model:
    def __init__(self, height) -> None:
        self.x_size = 20
        self.z_size = 20
        self.height = height
        self.noise = PerlinNoise()

        self.vertices  = []
        self.triangles = [0] * (self.x_size * self.z_size * 6)

    def gen_vertices(self):
        for z in range(0, self.z_size + 1):
            for x in range(0, self.x_size + 1):
                y = random.uniform(0, self.height) 
                y_perlin = self.noise([x * 0.3, z * 0.3]) * self.height
                self.vertices.append([x, y_perlin, z])
           
        verts_str = ""
        
        for v in self.vertices:
            verts_str += f'v {v[0]} {v[1]} {v[2]}\n'

        return verts_str

    def gen_triangles(self):
        vert = 0
        tris = 0
        
        tris_str = "f "
 
        for z in range(0, self.z_size):
            for x in range(0, self.x_size):
                self.triangles[tris + 0] = vert + 0
                self.triangles[tris + 1] = vert + self.x_size + 1
                self.triangles[tris + 2] = vert + 1
                self.triangles[tris + 3] = vert + 1
                self.triangles[tris + 4] = vert + self.x_size + 1
                self.triangles[tris + 5] = vert + self.x_size + 2

                tris_str += f'{self.triangles[tris + 0]}/{self.triangles[tris + 1]}/{self.triangles[tris + 2]} '
                tris_str += f'{self.triangles[tris + 3]}/{self.triangles[tris + 4]}/{self.triangles[tris + 5]} '

                tris += 6
                vert+=1
            vert += 1
  
        return tris_str.rstrip()

    def generate(self):
        verts = self.gen_vertices()
        tris = self.gen_triangles()

        obj_file = f"""
# Blender v3.5.1 OBJ File: ''
# www.blender.org
mtllib plane.mtl
o Plane
{verts}
vt 0.000000 0.000000
vt 1.000000 0.000000
vt 1.000000 1.000000
vt 0.000000 1.000000
vn 0.0000 1.0000 0.0000
usemtl None
s off
{tris}"""

        return obj_file
    
if __name__ == "__main__":
    m = Model(1.0)
    m.generate()
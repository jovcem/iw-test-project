from fastapi import FastAPI
from model import Model

app = FastAPI()

@app.get("/")
def read_root(height: float=1.0):
    obj_file = Model(height).generate()
    return {"data":obj_file}

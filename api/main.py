from typing import Union
from fastapi import FastAPI
from fastapi.staticfiles import StaticFiles
from model import Model

app = FastAPI()

app.mount("/static", StaticFiles(directory="static"), name="static")
 
@app.get("/")
def read_root(height: float=1.0):
    obj_file = Model(height).generate()
    return {"data":obj_file}


@app.get("/items/{item_id}")
def read_item(item_id: int, q: Union[str, None] = None):
    return {"item_id": item_id, "q": q}
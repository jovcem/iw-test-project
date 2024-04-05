from typing import Union
from fastapi import FastAPI
from fastapi.staticfiles import StaticFiles

app = FastAPI()

app.mount("/static", StaticFiles(directory="static"), name="static")

@app.get("/")
def read_root():
    data = {
        "vertices":[
            [-0.5,0,-0,5],
            [0.5,0,-0,5],
            [0.5,0,0,5],
            [-0.5,0,0,5],
        ],
        "triangles":[
            [0,2,1],
            [0,3,2]
        ]
    }
    return data


@app.get("/items/{item_id}")
def read_item(item_id: int, q: Union[str, None] = None):
    return {"item_id": item_id, "q": q}
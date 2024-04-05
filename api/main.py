from typing import Union
from fastapi import FastAPI
from fastapi.staticfiles import StaticFiles

app = FastAPI()

app.mount("/static", StaticFiles(directory="static"), name="static")

@app.get("/")
def read_root():
    posts = [
        {
            'userId':1,
            'id':1,
            'title':"this is title",
            'body':'this is the body'
        },
        {
            'userId':1,
            'id':2,
            'title':"this is title 2",
            'body':'this is the body 2'
        }
    ]
    return {"posts": posts}


@app.get("/items/{item_id}")
def read_item(item_id: int, q: Union[str, None] = None):
    return {"item_id": item_id, "q": q}
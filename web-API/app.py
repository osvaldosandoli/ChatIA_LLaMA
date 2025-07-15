from fastapi import FastAPI
from pydantic import BaseModel
from fastapi.middleware.cors import CORSMiddleware
import ollama

app = FastAPI(title="Chat LLaMA API")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # liberação para web .NET
    allow_methods=["*"],
    allow_headers=["*"],
)

class ChatRequest(BaseModel):
    message: str

class ChatResponse(BaseModel):
    reply: str

@app.post("/chat", response_model=ChatResponse)
def chat(req: ChatRequest):
    res = ollama.chat(model="llama3", messages=[
        {"role": "user", "content": req.message}
    ])
    return ChatResponse(reply=res['message']['content'])
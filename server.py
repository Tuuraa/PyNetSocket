import asyncio
import websockets


async def handler(websocket: websockets.WebSocketClientProtocol):
    print("websocket id: ", websocket.id, sep="", end="\n")
    await websocket.send("Hello, world!")


async def main():
    server = await websockets.serve(handler, "localhost", 8000)
    await server.wait_closed()


if __name__ == "__main__":
    asyncio.run(main())

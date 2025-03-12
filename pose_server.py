# import asyncio
# import websockets
# import json
# import numpy as np
#
# radius = 1.0  # 半径
# omega = 2 * np.pi / 2  # 2秒で1周する角速度
# async def send_pose(websocket, path):
#     t = 0
#     while True:
#         x = float(radius * np.cos(omega * t))  # numpy.float64 → float に変換
#         y = float(radius * np.sin(omega * t))
#         z = 0.0  # 平面上の動き（必要に応じて変える）
#         joints_3d = [x, y, z]
#         print(joints_3d)
#         await websocket.send(json.dumps(joints_3d))
#         await asyncio.sleep(0.03)
#         t += 0.03
#     print(2222)
#
# start_server = websockets.serve(send_pose, "192.168.160.12", 8765)
# asyncio.get_event_loop().run_until_complete(start_server)
# asyncio.get_event_loop().run_forever()

# import asyncio
# import json
# import numpy as np
#
# async def handle_client(reader, writer):
#     while True:
#         joints_3d = np.random.rand(3).tolist()
#         data = json.dumps(joints_3d) + "\n"  # JSONデータを改行で区切る
#         writer.write(data.encode("utf-8"))  # バイト列に変換して送信
#         await writer.drain()  # 送信バッファをフラッシュ
#         await asyncio.sleep(0.03)  # 30ms待機
#     print("Connection closed.")
#
# async def main():
#     server = await asyncio.start_server(handle_client, "192.168.160.12", 8765)
#     async with server:
#         await server.serve_forever()
#
# asyncio.run(main())

# import asyncio
# import json
# import numpy as np
#
# class UDPServerProtocol:
#     def __init__(self):
#         self.transport = None
#
#     def connection_made(self, transport):
#         self.transport = transport
#         print("UDP server started")
#
#     def datagram_received(self, data, addr):
#         """クライアントからデータを受け取る（基本的に何もしない）"""
#         # joints_3d = np.random.rand(3).tolist()  # 3D関節データを生成
#         message = "Hello World!"
#         response = json.dumps(message).encode("utf-8")
#         self.transport.sendto(response, addr)  # クライアントに送信
#
#     def error_received(self, exc):
#         print(f"Error received: {exc}")
#
#     def connection_lost(self, exc):
#         print("UDP server closed")
#
# async def main():
#     loop = asyncio.get_running_loop()
#     listen = await loop.create_datagram_endpoint(
#         lambda: UDPServerProtocol(), local_addr=("192.168.160.12", 8765)
#     )
#     await asyncio.sleep(float("inf"))  # サーバーを永続的に実行
#
# asyncio.run(main())

# import socket
# import pygame
#
# # 設定
# host = "192.168.160.29"
# port = 8765
#
# # ソケットの設定
# client_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
# client_socket.connect((host, port))
#
# # Pygameの初期化
# pygame.init()
# screen = pygame.display.set_mode((200, 200))
# pygame.display.set_caption("UDP Client")
#
# # メインループ
# running = True
# while running:
#     for event in pygame.event.get():
#         if event.type == pygame.QUIT:
#             running = False
#
#         # "S" キーが押されたらメッセージを送信
#         if event.type == pygame.KEYDOWN:
#             if event.key == pygame.K_s:
#                 print("Send")
#                 message = "Hello World!".encode('utf-8')
#                 client_socket.send(message)
#
# # クリーンアップ
# client_socket.close()
# pygame.quit()



import socket
import time
import numpy as np

# UDPの送信設定
UDP_IP = "192.168.252.116"  # WindowsマシンのIPアドレスに変更
UDP_PORT = 2469  # 任意のポート番号

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

radius = 1.0  # 半径
omega = 2 * np.pi / 2  # 2秒で1周する角速度
t = 0

frame_time = 1 / 60
last_time = time.time()

try:
    i = 0
    while True:
        # message = f"{i}"  # 送信するデータ（数値を文字列に変換）
        # sock.sendto(message.encode(), (UDP_IP, UDP_PORT))
        # print(f"Sent: {message}")
        # i += 1
        # position = np.random.rand(3).tolist()

        current_time = time.time()
        if current_time - last_time >= frame_time:
            x = float(radius * np.cos(omega * t))  # numpy.float64 → float に変換
            y = float(radius * np.sin(omega * t))
            z = 0.0  # 平面上の動き（必要に応じて変える）

            position = [x, y, z]
            position_bytes = bytes(str(position), 'utf-8')
            sock.sendto(position_bytes, (UDP_IP, UDP_PORT))
            print(f"Sent: {position_bytes}")
            # t += 1 / 60
            # time.sleep(1/60)  # 30FPSで送信
            t += frame_time  # 正確な時間増加
            last_time = current_time
except KeyboardInterrupt:
    print("\nStopped sending")
finally:
    sock.close()

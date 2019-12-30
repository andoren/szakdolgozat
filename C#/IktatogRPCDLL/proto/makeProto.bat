protoc -I=. --csharp_out=. ./iktato.proto --grpc_out=. --plugin=protoc-gen-grpc=./grpc_csharp_plugin.exe 
pause
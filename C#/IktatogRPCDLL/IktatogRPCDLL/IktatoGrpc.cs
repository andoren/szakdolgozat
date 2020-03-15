// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: iktato.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Iktato {
  public static partial class IktatoService
  {
    static readonly string __ServiceName = "Iktato.IktatoService";

    static readonly grpc::Marshaller<global::Iktato.LoginMessage> __Marshaller_Iktato_LoginMessage = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.LoginMessage.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Iktato.User> __Marshaller_Iktato_User = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.User.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Iktato.EmptyMessage> __Marshaller_Iktato_EmptyMessage = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.EmptyMessage.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Iktato.Answer> __Marshaller_Iktato_Answer = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.Answer.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Iktato.Ikonyv> __Marshaller_Iktato_Ikonyv = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.Ikonyv.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Iktato.RovidIkonyv> __Marshaller_Iktato_RovidIkonyv = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.RovidIkonyv.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Iktato.SearchIkonyvData> __Marshaller_Iktato_SearchIkonyvData = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.SearchIkonyvData.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Iktato.DeleteMessage> __Marshaller_Iktato_DeleteMessage = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.DeleteMessage.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Iktato.DocumentInfo> __Marshaller_Iktato_DocumentInfo = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.DocumentInfo.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Iktato.Document> __Marshaller_Iktato_Document = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Iktato.Document.Parser.ParseFrom);

    static readonly grpc::Method<global::Iktato.LoginMessage, global::Iktato.User> __Method_Login = new grpc::Method<global::Iktato.LoginMessage, global::Iktato.User>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Login",
        __Marshaller_Iktato_LoginMessage,
        __Marshaller_Iktato_User);

    static readonly grpc::Method<global::Iktato.EmptyMessage, global::Iktato.Answer> __Method_Logout = new grpc::Method<global::Iktato.EmptyMessage, global::Iktato.Answer>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Logout",
        __Marshaller_Iktato_EmptyMessage,
        __Marshaller_Iktato_Answer);

    static readonly grpc::Method<global::Iktato.User, global::Iktato.Answer> __Method_Register = new grpc::Method<global::Iktato.User, global::Iktato.Answer>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Register",
        __Marshaller_Iktato_User,
        __Marshaller_Iktato_Answer);

    static readonly grpc::Method<global::Iktato.Ikonyv, global::Iktato.RovidIkonyv> __Method_AddIktatas = new grpc::Method<global::Iktato.Ikonyv, global::Iktato.RovidIkonyv>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddIktatas",
        __Marshaller_Iktato_Ikonyv,
        __Marshaller_Iktato_RovidIkonyv);

    static readonly grpc::Method<global::Iktato.Ikonyv, global::Iktato.Answer> __Method_ModifyIktatas = new grpc::Method<global::Iktato.Ikonyv, global::Iktato.Answer>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ModifyIktatas",
        __Marshaller_Iktato_Ikonyv,
        __Marshaller_Iktato_Answer);

    static readonly grpc::Method<global::Iktato.EmptyMessage, global::Iktato.Ikonyv> __Method_ListallIktatas = new grpc::Method<global::Iktato.EmptyMessage, global::Iktato.Ikonyv>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "ListallIktatas",
        __Marshaller_Iktato_EmptyMessage,
        __Marshaller_Iktato_Ikonyv);

    static readonly grpc::Method<global::Iktato.SearchIkonyvData, global::Iktato.Ikonyv> __Method_ListIktatas = new grpc::Method<global::Iktato.SearchIkonyvData, global::Iktato.Ikonyv>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "ListIktatas",
        __Marshaller_Iktato_SearchIkonyvData,
        __Marshaller_Iktato_Ikonyv);

    static readonly grpc::Method<global::Iktato.DeleteMessage, global::Iktato.Answer> __Method_DeleteIktatas = new grpc::Method<global::Iktato.DeleteMessage, global::Iktato.Answer>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeleteIktatas",
        __Marshaller_Iktato_DeleteMessage,
        __Marshaller_Iktato_Answer);

    static readonly grpc::Method<global::Iktato.DocumentInfo, global::Iktato.Document> __Method_GetDocumentById = new grpc::Method<global::Iktato.DocumentInfo, global::Iktato.Document>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetDocumentById",
        __Marshaller_Iktato_DocumentInfo,
        __Marshaller_Iktato_Document);

    static readonly grpc::Method<global::Iktato.Document, global::Iktato.DocumentInfo> __Method_UploadDocument = new grpc::Method<global::Iktato.Document, global::Iktato.DocumentInfo>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UploadDocument",
        __Marshaller_Iktato_Document,
        __Marshaller_Iktato_DocumentInfo);

    static readonly grpc::Method<global::Iktato.DocumentInfo, global::Iktato.Answer> __Method_Removedocument = new grpc::Method<global::Iktato.DocumentInfo, global::Iktato.Answer>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Removedocument",
        __Marshaller_Iktato_DocumentInfo,
        __Marshaller_Iktato_Answer);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Iktato.IktatoReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of IktatoService</summary>
    [grpc::BindServiceMethod(typeof(IktatoService), "BindService")]
    public abstract partial class IktatoServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Iktato.User> Login(global::Iktato.LoginMessage request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Iktato.Answer> Logout(global::Iktato.EmptyMessage request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Iktato.Answer> Register(global::Iktato.User request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Iktato.RovidIkonyv> AddIktatas(global::Iktato.Ikonyv request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Iktato.Answer> ModifyIktatas(global::Iktato.Ikonyv request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task ListallIktatas(global::Iktato.EmptyMessage request, grpc::IServerStreamWriter<global::Iktato.Ikonyv> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task ListIktatas(global::Iktato.SearchIkonyvData request, grpc::IServerStreamWriter<global::Iktato.Ikonyv> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Iktato.Answer> DeleteIktatas(global::Iktato.DeleteMessage request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Iktato.Document> GetDocumentById(global::Iktato.DocumentInfo request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Iktato.DocumentInfo> UploadDocument(global::Iktato.Document request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Iktato.Answer> Removedocument(global::Iktato.DocumentInfo request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for IktatoService</summary>
    public partial class IktatoServiceClient : grpc::ClientBase<IktatoServiceClient>
    {
      /// <summary>Creates a new client for IktatoService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public IktatoServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for IktatoService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public IktatoServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected IktatoServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected IktatoServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Iktato.User Login(global::Iktato.LoginMessage request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Login(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Iktato.User Login(global::Iktato.LoginMessage request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Login, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.User> LoginAsync(global::Iktato.LoginMessage request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return LoginAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.User> LoginAsync(global::Iktato.LoginMessage request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Login, null, options, request);
      }
      public virtual global::Iktato.Answer Logout(global::Iktato.EmptyMessage request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Logout(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Iktato.Answer Logout(global::Iktato.EmptyMessage request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Logout, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> LogoutAsync(global::Iktato.EmptyMessage request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return LogoutAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> LogoutAsync(global::Iktato.EmptyMessage request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Logout, null, options, request);
      }
      public virtual global::Iktato.Answer Register(global::Iktato.User request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Register(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Iktato.Answer Register(global::Iktato.User request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Register, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> RegisterAsync(global::Iktato.User request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RegisterAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> RegisterAsync(global::Iktato.User request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Register, null, options, request);
      }
      public virtual global::Iktato.RovidIkonyv AddIktatas(global::Iktato.Ikonyv request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddIktatas(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Iktato.RovidIkonyv AddIktatas(global::Iktato.Ikonyv request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AddIktatas, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.RovidIkonyv> AddIktatasAsync(global::Iktato.Ikonyv request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddIktatasAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.RovidIkonyv> AddIktatasAsync(global::Iktato.Ikonyv request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AddIktatas, null, options, request);
      }
      public virtual global::Iktato.Answer ModifyIktatas(global::Iktato.Ikonyv request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ModifyIktatas(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Iktato.Answer ModifyIktatas(global::Iktato.Ikonyv request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ModifyIktatas, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> ModifyIktatasAsync(global::Iktato.Ikonyv request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ModifyIktatasAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> ModifyIktatasAsync(global::Iktato.Ikonyv request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ModifyIktatas, null, options, request);
      }
      public virtual grpc::AsyncServerStreamingCall<global::Iktato.Ikonyv> ListallIktatas(global::Iktato.EmptyMessage request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ListallIktatas(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncServerStreamingCall<global::Iktato.Ikonyv> ListallIktatas(global::Iktato.EmptyMessage request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_ListallIktatas, null, options, request);
      }
      public virtual grpc::AsyncServerStreamingCall<global::Iktato.Ikonyv> ListIktatas(global::Iktato.SearchIkonyvData request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ListIktatas(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncServerStreamingCall<global::Iktato.Ikonyv> ListIktatas(global::Iktato.SearchIkonyvData request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_ListIktatas, null, options, request);
      }
      public virtual global::Iktato.Answer DeleteIktatas(global::Iktato.DeleteMessage request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteIktatas(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Iktato.Answer DeleteIktatas(global::Iktato.DeleteMessage request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DeleteIktatas, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> DeleteIktatasAsync(global::Iktato.DeleteMessage request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteIktatasAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> DeleteIktatasAsync(global::Iktato.DeleteMessage request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DeleteIktatas, null, options, request);
      }
      public virtual global::Iktato.Document GetDocumentById(global::Iktato.DocumentInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetDocumentById(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Iktato.Document GetDocumentById(global::Iktato.DocumentInfo request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetDocumentById, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Document> GetDocumentByIdAsync(global::Iktato.DocumentInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetDocumentByIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Document> GetDocumentByIdAsync(global::Iktato.DocumentInfo request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetDocumentById, null, options, request);
      }
      public virtual global::Iktato.DocumentInfo UploadDocument(global::Iktato.Document request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UploadDocument(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Iktato.DocumentInfo UploadDocument(global::Iktato.Document request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UploadDocument, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.DocumentInfo> UploadDocumentAsync(global::Iktato.Document request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UploadDocumentAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.DocumentInfo> UploadDocumentAsync(global::Iktato.Document request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UploadDocument, null, options, request);
      }
      public virtual global::Iktato.Answer Removedocument(global::Iktato.DocumentInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Removedocument(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Iktato.Answer Removedocument(global::Iktato.DocumentInfo request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Removedocument, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> RemovedocumentAsync(global::Iktato.DocumentInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RemovedocumentAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Iktato.Answer> RemovedocumentAsync(global::Iktato.DocumentInfo request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Removedocument, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override IktatoServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new IktatoServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(IktatoServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Login, serviceImpl.Login)
          .AddMethod(__Method_Logout, serviceImpl.Logout)
          .AddMethod(__Method_Register, serviceImpl.Register)
          .AddMethod(__Method_AddIktatas, serviceImpl.AddIktatas)
          .AddMethod(__Method_ModifyIktatas, serviceImpl.ModifyIktatas)
          .AddMethod(__Method_ListallIktatas, serviceImpl.ListallIktatas)
          .AddMethod(__Method_ListIktatas, serviceImpl.ListIktatas)
          .AddMethod(__Method_DeleteIktatas, serviceImpl.DeleteIktatas)
          .AddMethod(__Method_GetDocumentById, serviceImpl.GetDocumentById)
          .AddMethod(__Method_UploadDocument, serviceImpl.UploadDocument)
          .AddMethod(__Method_Removedocument, serviceImpl.Removedocument).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, IktatoServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Login, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Iktato.LoginMessage, global::Iktato.User>(serviceImpl.Login));
      serviceBinder.AddMethod(__Method_Logout, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Iktato.EmptyMessage, global::Iktato.Answer>(serviceImpl.Logout));
      serviceBinder.AddMethod(__Method_Register, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Iktato.User, global::Iktato.Answer>(serviceImpl.Register));
      serviceBinder.AddMethod(__Method_AddIktatas, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Iktato.Ikonyv, global::Iktato.RovidIkonyv>(serviceImpl.AddIktatas));
      serviceBinder.AddMethod(__Method_ModifyIktatas, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Iktato.Ikonyv, global::Iktato.Answer>(serviceImpl.ModifyIktatas));
      serviceBinder.AddMethod(__Method_ListallIktatas, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Iktato.EmptyMessage, global::Iktato.Ikonyv>(serviceImpl.ListallIktatas));
      serviceBinder.AddMethod(__Method_ListIktatas, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Iktato.SearchIkonyvData, global::Iktato.Ikonyv>(serviceImpl.ListIktatas));
      serviceBinder.AddMethod(__Method_DeleteIktatas, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Iktato.DeleteMessage, global::Iktato.Answer>(serviceImpl.DeleteIktatas));
      serviceBinder.AddMethod(__Method_GetDocumentById, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Iktato.DocumentInfo, global::Iktato.Document>(serviceImpl.GetDocumentById));
      serviceBinder.AddMethod(__Method_UploadDocument, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Iktato.Document, global::Iktato.DocumentInfo>(serviceImpl.UploadDocument));
      serviceBinder.AddMethod(__Method_Removedocument, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Iktato.DocumentInfo, global::Iktato.Answer>(serviceImpl.Removedocument));
    }

  }
}
#endregion

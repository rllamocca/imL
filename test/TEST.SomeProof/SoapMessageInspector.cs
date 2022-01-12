//using System.ServiceModel;
//using System.ServiceModel.Channels;
//using System.ServiceModel.Description;
//using System.ServiceModel.Dispatcher;

//namespace MX.Frotcom.ToPosition
//{
//    public class SoapMessageInspector : IClientMessageInspector
//    {
//        public string LastRequestXml { get; private set; }
//        public string LastResponseXml { get; private set; }

//        public object BeforeSendRequest(ref Message request, IClientChannel channel)
//        {
//            LastRequestXml = request.ToString();
//            //Console.WriteLine("LastRequestXml:");
//            //Console.WriteLine(LastRequestXml);

//            return request;
//        }

//        public void AfterReceiveReply(ref Message reply, object correlationState)
//        {
//            LastResponseXml = reply.ToString();
//            Console.WriteLine("LastResponseXml:");
//            Console.WriteLine(LastResponseXml);
//        }
//    }

//    public class SoapInspectorBehavior : IEndpointBehavior
//    {
//        private readonly SoapMessageInspector inspector_ = new SoapMessageInspector();

//        public string LastRequestXml => inspector_.LastRequestXml;
//        public string LastResponseXml => inspector_.LastResponseXml;

//        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
//        {
//        }

//        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
//        {
//        }

//        public void Validate(ServiceEndpoint endpoint)
//        {
//        }

//        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
//        {
//            clientRuntime.ClientMessageInspectors.Add(inspector_);
//        }
//    }

//    public class ServerMessageLogger : IDispatchMessageInspector
//    {
//        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
//        {
//            Console.WriteLine(request);
//            return null;
//        }

//        public void BeforeSendReply(ref Message reply, object correlationState)
//        {
//            Console.WriteLine(reply);
//        }
//    }

//    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
//    public class CustContractBehaviorAttribute : Attribute, IContractBehavior, //IContractBehaviorAttribute,
//                                                                               IOperationBehavior
//    {
//        public Type TargetContract => throw new NotImplementedException();

//        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
//        {
//            return;
//        }

//        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
//        {
//            throw new NotImplementedException();
//        }

//        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
//        {
//            clientRuntime.ClientMessageInspectors.Add(new SoapMessageInspector());
//        }

//        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
//        {
//            throw new NotImplementedException();
//        }

//        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
//        {
//            dispatchRuntime.MessageInspectors.Add(new ServerMessageLogger());
//        }

//        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
//        {

//        }

//        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
//        {
//            return;
//        }

//        public void Validate(OperationDescription operationDescription)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
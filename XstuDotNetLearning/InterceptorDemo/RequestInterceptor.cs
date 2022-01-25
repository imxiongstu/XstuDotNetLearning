using Castle.DynamicProxy;

namespace InterceptorDemo
{
    public class RequestInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine(1);
            invocation.Proceed();
            Console.WriteLine(2);
        }
    }
}

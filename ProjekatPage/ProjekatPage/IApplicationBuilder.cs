using System;

namespace ProjekatPage
{
    public interface IApplicationBuilder
    {
        void UseCors(Action<object> p);
    }
}
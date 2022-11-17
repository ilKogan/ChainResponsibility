/// <summary>
/// Базовый класс обрабочика в цепи 
/// </summary>
/// <typeparam name="D"> данные с которыми работает обработчик</typeparam>
public abstract class Handler<A,D> 
{
    public A id;
    /// <summary>
    /// Выполнить обработку над данными
    /// </summary>
    /// <param name="data">обрабатываемые данные </param>
    public abstract void Execute(ref D data);
}













///------------






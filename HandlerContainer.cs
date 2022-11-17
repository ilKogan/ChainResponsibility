
/// <summary>
/// Базовый класс для хранения обработчиков и последовательного их выполнения
/// </summary>
/// <typeparam name="D"> данные с которыми работает обработчики</typeparam>
public class HandlerContainer<A, D> : Handler<A, D>
{
    Handler<A, D>[] elements;

    public HandlerContainer(Handler<A, D>[] elements)
    {
        this.elements = elements;
    }

    public override void Execute(ref D value)
    {
        D result = value;
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].Execute(ref result);
        }
        value = result;
    }
}













///------------






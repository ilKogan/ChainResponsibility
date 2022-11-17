
/// <summary>
/// Универсальный класс отвечающий за модифицированную реализацию цепочки обработчиков и управление ими
/// </summary>
/// <typeparam name="D">тип данных которые будут обрабатывать обработчики</typeparam>
public class ChainResponsibility<A, D>
{
    private List<Handler<A, D>> chains;

    public ChainResponsibility(int length)
    {
        chains = new List<Handler<A, D>>();
    }


    /// <summary>
    /// Запуск цепи обработчиков
    /// </summary>
    /// <param name="value">входные данные над которыми будет производиться обработка</param>
    /// <returns></returns>
    public D Run(D value)
    {
        D result = value;

        foreach (Handler<A, D> handler in chains)
        {
            handler.Execute(ref result);
            Console.WriteLine($"Execute {handler.id}");
        }

        return result;
    }

    /// <summary>
    /// Добавление обработчика
    /// </summary>
    /// <param name="addres">индекс елемента массива куда будет добавлен обработчик </param>
    /// <param name="element">обработчик</param>
    public void Add( Handler<A, D> element)
    {
        chains.Add(element);
    }

    /// <summary>
    /// Добавление нескольких обработчиков доступных по одному адресу
    /// </summary>
    /// <param name="addres">индекс елемента массива куда будет добавлен контейнер с обработчиками </param>
    /// <param name="elements">комбинация обработчиков</param>
    public void AddCombination(HandlerContainer<A, D> elements)
    {
        chains.Add(elements);
    }

    /// <summary>
    /// Добавление нескольких обработчиков доступных по одному адресу
    /// </summary>
    /// <param name="id"> индекс елемента массива куда будет добавлена комбинация обработчиков </param>
    /// <param name="elements"> комбинация обработчиков </param>
    public void AddCombination(Handler<A,D>[] elements)
    {
        HandlerContainer<A, D> combination = new HandlerContainer<A, D>(elements);
        chains.Add(combination);
    }

    /// <summary>
    /// Удаление элемента из цепочки
    /// </summary>
    /// <param name="id">индекс елемента</param>
    public void Remove(A? key)
    {
        var v = chains.Find((c) => c.id.Equals(key));
        chains.Remove(v);
    }

}













///------------






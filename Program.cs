using System;
using System.Collections.Generic;
using System.Diagnostics;


public enum Modificator
{
    APATHY,
    GEMOTAGEN,
    DEADLINE,
    INVESTMENT,
    FURY,
    DISLIKE,
    M1,
    M2,
    M3,
    M4,
    M5,
    M6,

}

public class StatsHandler : Handler<Modificator, CharacterData>
{
    int factor;
    Stats stats;
    Operation operation;


    public StatsHandler(Modificator id,Operation op, Stats stats, int factor)
    {
        this.id = id;
        this.factor = factor;
        this.stats = stats;
        this.operation = op;
    }

    public override void Execute(ref CharacterData value)
    {
        float v = value.GetStats(stats);
        switch (operation)
        {
            case Operation.ADD:
                value.SetStats(stats, Add(v));
                break;
            case Operation.ADD_PERCENT:
                value.SetStats(stats, AddPercent(v));
                break;
            case Operation.SUB:
                value.SetStats(stats, Sub(v));
                break;
            case Operation.SUB_PERCENT:
                value.SetStats(stats, SubPercent(v));
                break;
            case Operation.MUL:
                value.SetStats(stats, Mul(v));
                break;
            case Operation.DIV:
                value.SetStats(stats, Div(v));
                break;
        }
    }

    private float Add(float x)
    {
        return x + factor;
    }
    private float Sub(float x)
    {
        return x - factor;
    }
    private float AddPercent(float x)
    {
        float percent = x / 100;
        x += percent * factor;
        return x;
    }
    private float SubPercent(float x)
    {
        float percent = x / 100;
        x -= percent * factor;
        return x;
    }
    private float Mul(float x)
    {
        return x * factor;
    }
    private float Div(float x)
    {
        return x / factor;
    }
}

public static class Program
{
    const int MAX_MODIFICATOR_COUNT = 500;
    static ChainResponsibility<Modificator,CharacterData> chain = new ChainResponsibility<Modificator, CharacterData>(MAX_MODIFICATOR_COUNT);
    static Stopwatch timer = new Stopwatch();

    public static void Main(string[] arg)
    {
        CharacterData defaultValue = new CharacterData(10, 5, 2);
        CharacterData chainResult;
        Console.WriteLine($"{defaultValue}\n");

        chain.Add(new StatsHandler(Modificator.DISLIKE,Operation.ADD, Stats.HEALTH, 1));
        chain.Add(new StatsHandler(Modificator.FURY,Operation.ADD, Stats.HEALTH, 1));
        chain.Add(new StatsHandler(Modificator.INVESTMENT,Operation.ADD, Stats.HEALTH, 1));
        chain.Add(new StatsHandler(Modificator.DEADLINE,Operation.ADD, Stats.HEALTH, 1));
        chain.Add(new StatsHandler(Modificator.GEMOTAGEN,Operation.ADD, Stats.HEALTH, 1));
        chain.Add(new StatsHandler(Modificator.APATHY,Operation.ADD, Stats.HEALTH, 1));

        chain.AddCombination(TestFunc());
        chain.AddCombination(TestFunc2());
        chain.Remove(Modificator.FURY);
        TestManyElements();
        
        
        timer.Start();
        chainResult = chain.Run(defaultValue);
        timer.Stop();


        Console.WriteLine($"\n time:{timer.Elapsed}");
        Console.WriteLine($"\n tics:{timer.ElapsedTicks}");
    }


    public static StatsHandler[] TestFunc()
    {
        StatsHandler[] handlers = new StatsHandler[]
        {
            new StatsHandler(Modificator.M1,Operation.ADD, Stats.SPEED, 1),
            new StatsHandler(Modificator.M2,Operation.SUB, Stats.DAMAGE, 2),
            new StatsHandler(Modificator.M3,Operation.ADD_PERCENT, Stats.HEALTH, 1)
        };
        return handlers;
    }

    public static HandlerContainer<Modificator, CharacterData> TestFunc2()
    {
        StatsHandler[] handlers = new StatsHandler[]
        {
            new StatsHandler(Modificator.M4,Operation.ADD, Stats.SPEED, 1),
            new StatsHandler(Modificator.M5,Operation.SUB, Stats.DAMAGE, 2),
            new StatsHandler(Modificator.M6,Operation.ADD_PERCENT, Stats.HEALTH, 1)
        };
        return new HandlerContainer<Modificator,CharacterData>(handlers);
    }

    public static void TestManyElements()
    {
        int test = 0;
        for (int i = 0; i < MAX_MODIFICATOR_COUNT; i++)
        {
            test = test == 5 ? 0 : test + 1;
            switch (test)
            {
                case 0:
                    chain.Add(new StatsHandler(Modificator.M1,Operation.ADD, Stats.HEALTH, 2));
                    break;
                case 1:
                    chain.Add(new StatsHandler(Modificator.M2,Operation.SUB, Stats.HEALTH, 2));
                    break;
                case 2:
                    chain.Add(new StatsHandler(Modificator.M3,Operation.ADD_PERCENT, Stats.HEALTH, 3));
                    break;
                case 3:
                    chain.Add(new StatsHandler(Modificator.M4,Operation.SUB_PERCENT, Stats.HEALTH, 1));
                    break;
                case 4:
                    chain.Add(new StatsHandler(Modificator.M5,Operation.MUL, Stats.HEALTH, 2));
                    break;
                case 5:
                    chain.Add(new StatsHandler(Modificator.M6,Operation.DIV, Stats.HEALTH, 2));
                    break;
            }

        }
    }

}
















///------------






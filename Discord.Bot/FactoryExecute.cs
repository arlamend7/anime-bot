namespace Discord.Bot
{
    public class FactoryExecute
    {
        public void Add(Action execution)
        {            
             Task.Run(execution);
        }
    }
}

using Quartz;
using Serilog.Context;

namespace CDL_Integration.Workers.Logs
{
    public class LogsJobListener : IJobListener
    {
        private readonly ILogger logger;

        public LogsJobListener(ILogger<LogsJobListener> logger)
        {
            this.logger = logger;
        }
        public string Name => "LogsJobsListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            LogContext.PushProperty("TransactionId", context.FireInstanceId);
            
            DateTime? fireTime = context.FireTimeUtc.LocalDateTime;
            this.logger.LogInformation("<{EventoId}> {Job} - Iniciado em {FireTime}",
                "JobToBeExecuted",
                context.JobDetail.JobType.Name,
                fireTime
                );

            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            LogContext.PushProperty("TransactionId", context.FireInstanceId);

            try
            {
                this.logger.LogInformation("<{EventoId}> {Job} -  Executado em {Elapsed:0.0000} segundos",
                       "JobWasExecuted",
                       context.JobDetail.JobType.Name,
                       context.JobRunTime.TotalSeconds);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "<{EventoId}> - Ocorreu um erro interno.", "JobWasExecuted");
            }

            return Task.CompletedTask;
        }
    }
}


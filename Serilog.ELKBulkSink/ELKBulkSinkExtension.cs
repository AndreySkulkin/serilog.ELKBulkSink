﻿using System;
using Serilog.Configuration;
using Serilog.Events;

namespace Serilog.ELKBulkSink
{
    public static class ELKBulkSinkExtension
    {
        /// <summary>
        ///  Create a new Loggly Bulk Sink which uses the HTTP Bulk Protocol
        /// </summary>
        /// <param name="lc">Logger Configuration</param>
        /// <param name="elkUrl">ELK gate url</param>
        /// <param name="index">Index template</param>
        /// <param name="restrictedToMinLevel">Minimum Log Level to Restrict to </param>
        /// <param name="batchPostingLimit">Batch Posting Limit, defaults to 1000</param>
        /// <param name="period">Frequency of Periodic Batch Sink auto flushing</param>
        /// <param name="includeDiagnostics">Whether or not to send the Loggly Diagnostics Event</param>
        /// <returns>Original Log Sink Configuration now updated</returns>
        /// <remarks>Depending on your aveage log event size, a batch positing limit on the order of 10000 could be reasonable</remarks>
        public static LoggerConfiguration ELKBulk(this LoggerSinkConfiguration lc, 
            string elkUrl, 
            string indexTemplate,
            LogEventLevel restrictedToMinLevel = LogEventLevel.Verbose,
            int batchPostingLimit = 1000,
            TimeSpan? period = null,
            bool includeDiagnostics = false)
        {
            if (lc == null) throw new ArgumentNullException("lc");

            var frequency = period ?? TimeSpan.FromSeconds(30);

            return lc.Sink(new ELKSink(elkUrl, indexTemplate, batchPostingLimit, frequency, includeDiagnostics), restrictedToMinLevel);
        }
    }
}
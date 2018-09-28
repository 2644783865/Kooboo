﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading;

namespace Kooboo.HttpServer
{
    public class InlineLoggingThreadPool : KoobooThreadPool
    {
        private readonly ILogger _log;

        public InlineLoggingThreadPool(ILogger log)
        {
            _log = log;
        }

        public override void Run(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                _log.LogError(0, e, "InlineLoggingThreadPool.Run");
            }
        }

        public override void UnsafeRun(WaitCallback action, object state)
        {
            action(state);
        }

        public override void Schedule(Action action)
        {
            Run(action);
        }

        public override void Schedule(Action<object> action, object state)
        {
            try
            {
                action(state);
            }
            catch (Exception e)
            {
                _log.LogError(0, e, "InlineLoggingThreadPool.Schedule");
            }
        }
    }
}

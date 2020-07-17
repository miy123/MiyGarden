//using System;
//using System.Collections.Generic;
//using System.Diagnostics.Contracts;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace MiyGarden.Service.MsSource
//{
//    public class MyTask
//    {
//        public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
//        {
//            // Throw on non-sensical time
//            if (millisecondsDelay < -1)
//            {
//                throw new ArgumentOutOfRangeException("millisecondsDelay", Environment.GetResourceString("Task_Delay_InvalidMillisecondsDelay"));
//            }
//            Contract.EndContractBlock();

//            // some short-cuts in case quick completion is in order
//            if (cancellationToken.IsCancellationRequested)
//            {
//                // return a Task created as already-Canceled
//                return Task.FromCancellation(cancellationToken);
//            }
//            else if (millisecondsDelay == 0)
//            {
//                // return a Task created as already-RanToCompletion
//                return Task.CompletedTask;
//            }

//            // Construct a promise-style Task to encapsulate our return value
//            var promise = new DelayPromise(cancellationToken);

//            // Register our cancellation token, if necessary.
//            if (cancellationToken.CanBeCanceled)
//            {
//                promise.Registration = cancellationToken.InternalRegisterWithoutEC(state => ((DelayPromise)state).Complete(), promise);
//            }

//            // ... and create our timer and make sure that it stays rooted.
//            if (millisecondsDelay != Timeout.Infinite) // no need to create the timer if it's an infinite timeout
//            {
//                promise.Timer = new Timer(state => ((DelayPromise)state).Complete(), promise, millisecondsDelay, Timeout.Infinite);
//                promise.Timer.KeepRootedWhileScheduled();
//            }

//            // Return the timer proxy task
//            return promise;
//        }

//        [FriendAccessAllowed]
//        internal static Task FromCancellation(CancellationToken cancellationToken)
//        {
//            if (!cancellationToken.IsCancellationRequested) throw new ArgumentOutOfRangeException("cancellationToken");
//            Contract.EndContractBlock();
//            return new Task(true, TaskCreationOptions.None, cancellationToken);
//        }

//        internal struct VoidTaskResult { }

//        private sealed class DelayPromise : Task<VoidTaskResult>
//        {
//            internal DelayPromise(CancellationToken token)
//                : base()
//            {
//                this.Token = token;
//                if (AsyncCausalityTracer.LoggingOn)
//                    AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, this.Id, "Task.Delay", 0);

//                if (Task.s_asyncDebuggingEnabled)
//                {
//                    AddToActiveTasks(this);
//                }
//            }

//            internal readonly CancellationToken Token;
//            internal CancellationTokenRegistration Registration;
//            internal Timer Timer;

//            internal void Complete()
//            {
//                // Transition the task to completed.
//                bool setSucceeded;

//                if (Token.IsCancellationRequested)
//                {
//                    setSucceeded = TrySetCanceled(Token);
//                }
//                else
//                {
//                    if (AsyncCausalityTracer.LoggingOn)
//                        AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, this.Id, AsyncCausalityStatus.Completed);

//                    if (Task.s_asyncDebuggingEnabled)
//                    {
//                        RemoveFromActiveTasks(this.Id);
//                    }
//                    setSucceeded = TrySetResult(default(VoidTaskResult));
//                }

//                // If we won the ----, also clean up.
//                if (setSucceeded)
//                {
//                    if (Timer != null) Timer.Dispose();
//                    Registration.Dispose();
//                }
//            }
//        }
//    }
//}

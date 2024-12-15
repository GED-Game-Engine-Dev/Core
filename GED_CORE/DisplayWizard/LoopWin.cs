using GED.Core.SanityCheck;

namespace GED.Core.DisplayWizard {
    /// <summary>
    /// Create a loop function separated in nutshell. <br/>
    /// It will not actually start the loop by just instantiating it.
    /// </summary>
    public interface LoopBase
    {
        /// <summary>
        /// Loop as initiation.
        /// </summary>
        public abstract byte LoopBaseStart();

        /// <summary>
        /// Loop on End.
        /// </summary>
        public abstract byte LoopBaseEnd();


        /// <summary>
        /// Called separated from <see cref="LoopBaseUpdateTask"/>. <br/>
        /// </summary>
        /// <param name="err">Status code output buffer</param>
        /// <returns>
        /// Whether to continue the loop or not. <br/>
        /// When set to true will consider this class to continue.
        /// </returns>
        public abstract bool LoopBaseUpdate(out byte err);

        /// <summary>
        /// Customable waitable task during update. <br/>
        /// Must be independent from <see cref="LoopBaseUpdate"/>.
        /// </summary>
        /// <returns>Task to wait.</returns>
        public async virtual Task<byte> LoopBaseUpdateTask() {
            return States.OK;
        }

        /// <summary>
        /// Skeleton code for task loop.
        /// </summary>
        public async Task<byte> LoopBaseSpan() {
            bool flag = true;
            byte err = States.OK;
            err |= LoopBaseStart();
            if(States.IsActuallyOk(err) != States.OK)
            goto FLAG_DONE;

            while(flag) {
                Task<byte> task = LoopBaseUpdateTask();
                flag = LoopBaseUpdate(out err);
                await task;
                if(States.IsActuallyOk(err) != States.OK)
                goto FLAG_DONE;
            }
            
            FLAG_DONE:
            err |= LoopBaseEnd();
            return err;
        }
    }

    public abstract class LoopWin : MinCtrlWin, LoopBase
    {
        public LoopWin(
            out int err, 
            int VisualWidth, 
            int VisualHeight
        ) : base(
            out err, 
            VisualWidth, 
            VisualHeight
        ) => Task.Run(((LoopBase)this).LoopBaseSpan);

        public abstract byte LoopBaseEnd();
        public abstract byte LoopBaseStart();
        public abstract bool LoopBaseUpdate(out byte err);
        public async virtual Task<byte> LoopBaseUpdateTask() {
            return States.OK;
        }
    }
}
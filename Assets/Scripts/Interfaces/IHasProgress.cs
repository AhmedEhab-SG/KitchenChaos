using System;

public interface IHasProgress {
    public event EventHandler<OnProcessChangedEventArgs> OnProcessChanged;
    public class OnProcessChangedEventArgs : EventArgs {
        public float progressNormalized;
    }

}
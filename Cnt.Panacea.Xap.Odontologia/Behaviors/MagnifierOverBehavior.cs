using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;
using ShaderEffectsLibrary;

namespace Behaviors
{
    public class MagnifierOverBehavior : Behavior<FrameworkElement>
    {


        

        private Magnifier magnifier;
        private double originalMagnification;
        #region activo (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool? activo
        {
            get { return (bool?)GetValue(activoProperty); }
            set { SetValue(activoProperty, value); }
        }
        public static readonly DependencyProperty activoProperty =
            DependencyProperty.Register("activo", typeof(bool?), typeof(MagnifierOverBehavior),
            new PropertyMetadata(true, new PropertyChangedCallback(OnactivoChanged)));

        private static void OnactivoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MagnifierOverBehavior)d).OnactivoChanged(e);
        }

        protected virtual void OnactivoChanged(DependencyPropertyChangedEventArgs e)
        {
            activarLupa = (bool?)e.NewValue;
        }

        #endregion


        public MagnifierOverBehavior() :
            base()
        {
            this.magnifier = new Magnifier();
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseEnter += new MouseEventHandler( AssociatedObject_MouseEnter );
            this.AssociatedObject.MouseLeave += new MouseEventHandler( AssociatedObject_MouseLeave );
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.MouseEnter -= new MouseEventHandler( AssociatedObject_MouseEnter );
            this.AssociatedObject.MouseLeave -= new MouseEventHandler( AssociatedObject_MouseLeave );
        }

        private void AssociatedObject_MouseLeave( object sender, MouseEventArgs e )
        {
            if (activarLupa == true)
            {
                this.AssociatedObject.MouseMove -= new MouseEventHandler(AssociatedObject_MouseMove);
                this.AssociatedObject.Effect = null;
            }
        }

        private void AssociatedObject_MouseEnter( object sender, MouseEventArgs e )
        {
            if (activarLupa == true)
            {
                this.AssociatedObject.MouseMove += new MouseEventHandler(AssociatedObject_MouseMove);
                this.AssociatedObject.Effect = this.magnifier;
            }
        }

        private void AssociatedObject_MouseMove( object sender, MouseEventArgs e )
        {
            if (activarLupa == true)
            {
                (this.AssociatedObject.Effect as Magnifier).Center =
                    e.GetPosition(this.AssociatedObject);

                Point mousePosition = e.GetPosition(this.AssociatedObject);
                mousePosition.X /= this.AssociatedObject.ActualWidth;
                mousePosition.Y /= this.AssociatedObject.ActualHeight;
                this.magnifier.Center = mousePosition;

                Storyboard zoomInStoryboard = new Storyboard();
                DoubleAnimation zoomInAnimation = new DoubleAnimation();
                zoomInAnimation.To = this.magnifier.Magnification;
                zoomInAnimation.Duration = TimeSpan.FromSeconds(0.5);
                Storyboard.SetTarget(zoomInAnimation, this.AssociatedObject.Effect);
                Storyboard.SetTargetProperty(zoomInAnimation, new PropertyPath(Magnifier.MagnificationProperty));
                zoomInAnimation.FillBehavior = FillBehavior.HoldEnd;
                zoomInStoryboard.Children.Add(zoomInAnimation);
                zoomInStoryboard.Begin();
            }
        }

        private bool? _activarLupa = true;

        public bool? activarLupa
        {
            get { return _activarLupa; }
            set { _activarLupa= value; }
        }
        
    }
}

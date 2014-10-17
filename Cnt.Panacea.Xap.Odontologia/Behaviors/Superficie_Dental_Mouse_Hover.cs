using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia.Behaviors
{
    public class Superficie_Dental_Mouse_Hover :  TargetedTriggerAction<Shape>
    {
        private Storyboard Sb = new Storyboard();
        private ColorAnimation Color = new ColorAnimation();


        #region Color_Hover (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public SolidColorBrush Color_Hover
        {
            get { return (SolidColorBrush)GetValue(Color_HoverProperty); }
            set { SetValue(Color_HoverProperty, value); }
        }
        public static readonly DependencyProperty Color_HoverProperty =
            DependencyProperty.Register("Color_Hover", typeof(SolidColorBrush), typeof(Superficie_Dental_Mouse_Hover),
            new PropertyMetadata(new SolidColorBrush(Colors.Red), new PropertyChangedCallback(OnColor_HoverChanged)));

        private static void OnColor_HoverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Superficie_Dental_Mouse_Hover)d).OnColor_HoverChanged(e);
        }

        protected virtual void OnColor_HoverChanged(DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion


        protected override void OnAttached()
        {
            base.OnAttached();
            shape = (this.AssociatedObject as Shape);

         
        }

        void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
        {
            Sb.Begin();
        }

        protected override void OnDetaching()
        {
            base.OnAttached();
            var shape = (this.AssociatedObject as Shape);


            shape.MouseEnter -= AssociatedObject_MouseEnter;
            shape.MouseLeave -= AssociatedObject_MouseLeave;
        }

        protected override void Invoke(object parameter)
        {
            shape = (parameter as Shape);

            var Padre = (Grid)shape.Parent;
            
            shape.Fill = new SolidColorBrush(Colors.Blue);
            Color.SetValue(Storyboard.TargetNameProperty, shape.Name);
            Color.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("(Shape.Fill).(SolidColorBrush.Color)"));
            Color.To = Color_Hover.Color;


            Sb.Children.Add(Color);
            Sb.RepeatBehavior = RepeatBehavior.Forever;
            Padre.Resources.Add("Sb", Sb);

            shape.MouseEnter += AssociatedObject_MouseEnter;
            shape.MouseLeave += AssociatedObject_MouseLeave;
            
        }        

        public Shape shape { get; set; }
    }
}

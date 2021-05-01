using System;
using System.Reflection;
using System.Windows.Input;
using Weather.Data;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Weather.Helper.Behavior
{
	[Preserve(AllMembers = true)]
	public class PageCommandBehavior : BehaviorBase<ContentPage>
	{
		#region Fields

		Delegate eventHandler;

		#endregion

		#region Bindable Properties

		public static readonly BindableProperty EventNameProperty = BindableProperty.Create(nameof(EventName), typeof(string), typeof(PageCommandBehavior), null, propertyChanged: OnEventNameChanged);
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PageCommandBehavior), null);

		#endregion

		#region Constructor

		public PageCommandBehavior()
		{

		}

		#endregion

		#region Properties

		public string EventName
		{
			get { return (string)GetValue(EventNameProperty); }
			set { SetValue(EventNameProperty, value); }
		}

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}


		#endregion

		#region Override Methods

		protected override void OnAttachedTo(ContentPage bindable)
		{
			base.OnAttachedTo(bindable);
			RegisterEvent(EventName);
		}

		protected override void OnDetachingFrom(ContentPage bindable)
		{
			base.OnDetachingFrom(bindable);
			DeregisterEvent(EventName);
		}

		#endregion

		#region Callback Methods

		static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var behavior = (PageCommandBehavior)bindable;
			if (behavior.AssociatedObject == null) { return; }

			behavior.DeregisterEvent((string)oldValue);
			behavior.RegisterEvent((string)newValue);
		}

		#endregion

		#region Private Methods

		private void RegisterEvent(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) { return; }

			EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
			if (eventInfo == null)
			{
				throw new ArgumentException(string.Format(AppConstants.PageBehaviorRegisterError, EventName));
			}

			MethodInfo methodInfo = typeof(PageCommandBehavior).GetTypeInfo().GetDeclaredMethod(AppConstants.OnEvent);
			eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
			eventInfo.AddEventHandler(AssociatedObject, eventHandler);
		}

		private void DeregisterEvent(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) { return; }

			if (eventHandler == null) { return; }

			EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
			if (eventInfo == null)
			{
				throw new ArgumentException(string.Format(AppConstants.PageBehaviorDeRegisterError, EventName));
			}

			eventInfo.RemoveEventHandler(AssociatedObject, eventHandler);
			eventHandler = null;
		}

		private void OnEvent(object sender, object eventArgs)
		{
			if (Command == null) { return; }

			if (Command.CanExecute(eventArgs))
			{
				Command.Execute(eventArgs);
			}
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace Navix.Blazor.WebAssembly
{
    public sealed class BlazorPageNavigator : Navigator
    {
        private static readonly NavigatorSpecification NavSpec =
            new NavigatorSpecification(false, true, true);

        public override NavigatorSpecification Specification => NavSpec;
        
        private readonly NavigationManager _blazorNavManager;
        private NavigationIntent _rootIntent;

        private readonly Stack<NavigationIntent> _history = new Stack<NavigationIntent>();

        public BlazorPageNavigator(NavigationManager blazorNavManager)
        {
            _blazorNavManager = blazorNavManager;
            var uri = new Uri(blazorNavManager.BaseUri);
            _rootIntent = new NavigationIntent(uri.AbsolutePath);
            _history.Push(_rootIntent);
        }
        
        public BlazorPageNavigator(NavigationManager blazorNavManager, string rootPath = "/")
            :this(blazorNavManager, new NavigationIntent(rootPath))
        {
        }

        public BlazorPageNavigator(NavigationManager blazorNavManager, NavigationIntent rootIntent)
        {
            _blazorNavManager = blazorNavManager; 
            _rootIntent = rootIntent;
            _history.Push(_rootIntent);
        }

        public override void Forward(Screen screen, IScreenResolver resolver)
        {
            if (!(resolver is IBlazorScreenResolver blazorScreenResolver)) return;

            var intent = blazorScreenResolver.GetNavigationIntent(screen);
            _history.Push(intent);
            Dispatch(intent);
        }

        public override void Back()
        {
            if (_history.Count <= 1) return;
            _history.Pop();
            Dispatch(_history.Peek());
        }

        public override void BackToRoot()
        {
            if (_history.Count <= 1) return;

            var root = _history.Last();
            _history.Clear();
            Dispatch(root);
            _history.Push(root);
        }

        public override void Replace(Screen screen, IScreenResolver resolver)
        {
            if (!(resolver is IBlazorScreenResolver blazorScreenResolver)) return;

            var intent = blazorScreenResolver.GetNavigationIntent(screen);

            if (_history.Count > 0) _history.Pop();
            _history.Push(intent);

            if (_history.Count == 1)
                _rootIntent = intent;
            
            Dispatch(intent);
        }

        private void Dispatch(NavigationIntent intent)
        {
            var uriBuilder = new UriBuilder(_blazorNavManager.ToAbsoluteUri(intent.Path));
            var queryString = intent.ToQueryString();
            if (!string.IsNullOrEmpty(queryString))
                uriBuilder.Query = queryString;
            
            _blazorNavManager.NavigateTo(uriBuilder.Uri.ToString());
        }
    }
}
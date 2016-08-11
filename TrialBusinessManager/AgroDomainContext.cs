﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel;
using System.Reflection;
using System.ServiceModel;

namespace TrialBusinessManager.Web
{
    public partial class AgroDomainContext : DomainContext
    {
        //This is the place to set RIA Services query timeout. 
        //TimeSpan(0, 5, 0) means five minutes vs the default 60 sec
        partial void OnCreated()
        {
            TimeSpan tenMinutes = new TimeSpan(0, 10, 0);
            WcfTimeoutUtility.ChangeWcfSendTimeout(this, tenMinutes);
        }
    }
    public static class WcfTimeoutUtility
    {
        /// <summary>
        /// Changes the WCF endpoint SendTimeout for the specified domain
        /// context. 
        /// </summary>
        /// <param name="context">The domain context to modify.</param>
        /// <param name="sendTimeout">The new timeout value.</param>
        public static void ChangeWcfSendTimeout(DomainContext context, TimeSpan sendTimeout)
        {
            PropertyInfo channelFactoryProperty = context.DomainClient.GetType().GetProperty("ChannelFactory");
            if (channelFactoryProperty == null)
            {
                throw new InvalidOperationException("There is no 'ChannelFactory' property on the DomainClient.");
            }
            ChannelFactory factory = (ChannelFactory)channelFactoryProperty.GetValue(context.DomainClient, null);
            factory.Endpoint.Binding.SendTimeout = sendTimeout;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.MyGenericScripts.Framework.Messaging
{
    public class MessagePump
    {
        private readonly Dictionary<string, EventHandler<EventArgs>> _eventTable;
        private readonly List<ProdigyMessage> _messageQueue;
        private readonly List<ProdigyMessage> _messageIncomingQueue;

        private static MessagePump _instance;

        private MessagePump()
        {
            _messageQueue = new List<ProdigyMessage>();
            _messageIncomingQueue = new List<ProdigyMessage>();
            _eventTable = new Dictionary<string, EventHandler<EventArgs>>();
        }

        public static MessagePump Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MessagePump();
                }

                return _instance;
            }
        }

        protected bool RemoveIfDelivered(ProdigyMessage message)
        {
            if (message.IsDelivered)
            {
                _messageQueue.Remove(message);
                return true;
            }

            return false;
        }

        public void Update(float deltaTime)
        {
            AddMessagesToQueue();

            if (!_messageQueue.Any())
                return;

            ProcessMessages(deltaTime);
            _messageQueue.ForEach(msg => RemoveIfDelivered(msg));
        }

        protected void Invoke(ProdigyMessage message)
        {
            EventHandler<EventArgs> targetEvent;

            bool eventTableContainsId = _eventTable.TryGetValue(message.Id, out targetEvent);
            if (eventTableContainsId)
            {
                // Take a local copy to prevent a race condition if another thread
                // were to unsubscribe from this event.
                var callback = targetEvent;

                if (callback != null)
                {
                    callback(message.Sender, message.Data);
                }
            }
        }

        public void AddMessageToSystem(string messageId)
        {
            lock (_eventTable)
            {
                if (!_eventTable.ContainsKey(messageId))
                {
                    _eventTable.Add(messageId, null);
                }
            }
        }

        public void SendMessage(ProdigyMessage newMessage)
        {
            _messageIncomingQueue.Add(newMessage);
        }

        public int RegisterForMessage(string messageId, EventHandler<EventArgs> callBack)
        {
            lock (_eventTable)
            {
                if (_eventTable.ContainsKey(messageId))
                {
                    // Add the handler to the event.
                    _eventTable[messageId] = _eventTable[messageId] + callBack;
                    return 1; // REGISTER_MESSAGE_OK;
                }
            }

            return -1; // REGISTER_ERROR_MESSAGE_NOT_IN_SYSTEM;
        }

        public void UnRegisterForMessage(string messageId, EventHandler<EventArgs> callBack)
        {
            lock (_eventTable)
            {
                if (_eventTable.ContainsKey(messageId))
                {
                    _eventTable[messageId] = _eventTable[messageId] - callBack;

                    if (_eventTable[messageId] == null)
                    {
                        _eventTable.Remove(messageId);
                    }
                }
            }
        }

        //public void UnRegisterAll(int objectId)
        //{
        //    foreach (var messageType in _messageTypes.Values)
        //    {
        //        foreach (var messageRegister in messageType.MessageRegistrations)
        //        {
        //            if (messageRegister.ObjectId == objectId)
        //            {
        //                messageType.MessageRegistrations.Remove(messageRegister);
        //                //you can exit out here, there is only one registration 
        //                //allowed per message type
        //                return;
        //            }
        //        }
        //    }
        //}

        private void AddMessagesToQueue()
        {
            _messageIncomingQueue.Sort();
            _messageQueue.AddRange(_messageIncomingQueue);
            _messageIncomingQueue.Clear();
        }

        private void ProcessMessages(float gameTime)
        {
            foreach (var message in _messageQueue)
            {
                if (message.Timer > 0.0f)
                {
                    //delayed message, decrement timer
                    message.Timer -= gameTime;
                }
                else
                {
                    Invoke(message);
                    message.IsDelivered = true;
                }
            }
        }
    }
}


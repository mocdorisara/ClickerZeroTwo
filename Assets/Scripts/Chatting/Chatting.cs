using Game.Managers;
using Game.UI;
using Game.UI.Popup;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class ChattingMessage
    {
        public ChattingMessage(string id, string message)
        {
            this.id = id;
            this.message = message;
        }

        string id;
        string message;
    }


    public class Chatting
    {
        Queue<ChattingMessage> chattingQueue = new Queue<ChattingMessage>();
        HashSet<string> userSet = new HashSet<string>();
        List<string> userList = new List<string>();

        float dt;

        public void OnUpdate(float dt)
        {
            this.dt += dt;
            if (this.dt > 1)
            {
                CreateRandomAction();
                this.dt = 0;
            }
        }


        private void CreateRandomAction()
        {
            if (Random.Range(0, 100) < 70)
            {
                AddUser(Random.Range(0, 10000).ToString());
            }
            else
            {
                if (userList.Count == 0) return;

                string userId = userList[Random.Range(0, userList.Count)];
                OutUser(userId);
            }

            if (true)
            {
                if (userList.Count == 0) return;

                string userId = userList[Random.Range(0, userList.Count)];
                int length = Random.Range(0, 20);
                AddUserChatting(userId, RandomString(length));
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Range(0, s.Length)]).ToArray());
        }

        private void AddUser(string id)
        {
            if (userSet.Add(id))
            {
                userList.Add(id);

                GameManagers.PushCastMessage(new UICommandMessage(typeof(UI_StatusBar).Name, new UIStatusBarMessage() { action = "RefreshText" }));
            }
        }

        private void OutUser(string id)
        {
            if (userSet.Remove(id))
            {
                userList.Remove(id);
            }
        }

        private void AddUserChatting(string id, string message)
        {
            UIChattingMessage uiChattingMesssage = new UIChattingMessage();
            uiChattingMesssage.Id = id;
            uiChattingMesssage.Message = message;
            uiChattingMesssage.action = "AddMessage";

            GameManagers.PushCastMessage(new UICommandMessage(typeof(UI_Chatting).Name, uiChattingMesssage));
        }

        public int GetUserCount()
        {
            return userList.Count;
        }
    }
}



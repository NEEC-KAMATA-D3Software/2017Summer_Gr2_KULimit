using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using KuLimit.Device;

namespace KuLimit.Scene
{
    interface IScene
    {   //interfaceで定義したメソッドはpublic扱いである
        void Initialize();
        void Update(GameTime gametime);
        void Draw(Renderer renderer);
        void ShutDown();

        bool IsEnd();

        float PastSecond();
        Scene Next();
    }
}

using System;
using System.Threading.Tasks;

namespace Unity.Services.LevelPlay.Editor
{
    internal interface IOnLoadService
    {
        Task OnLoad();
    }
}

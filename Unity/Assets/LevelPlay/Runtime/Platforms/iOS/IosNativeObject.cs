using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using UnityEngine;

namespace com.unity3d.mediation
{
    public abstract class IosNativeObject : IDisposable
    {
        static ConcurrentDictionary<IntPtr, IosNativeObject> s_Objects = new ConcurrentDictionary<IntPtr, IosNativeObject>();

        IntPtr m_NativePtr;
        readonly bool m_UsesCallbacks;

        protected IosNativeObject(bool usesCallbacks)
        {
            // Some native objects (such as IosInterstitialLoadListener) do not receive events from Obj-c.
            // For that reason, it is not needed to add them to the static dictionary s_Objects.
            // This is to keep the number of objects in the dictionary small, for better performance.
            m_UsesCallbacks = usesCallbacks;
        }

        public IntPtr NativePtr
        {
            get => m_NativePtr;
            protected set
            {
                if (m_NativePtr == value) return;
                if (m_UsesCallbacks && m_NativePtr != IntPtr.Zero)
                {
                    s_Objects.TryRemove(m_NativePtr, out _);
                }
                m_NativePtr = value;
                if (m_UsesCallbacks && m_NativePtr != IntPtr.Zero)
                {
                    s_Objects.TryAdd(m_NativePtr, this);
                }
            }
        }

        protected static T Get<T>(IntPtr ptr) where T : IosNativeObject
        {
            return s_Objects.TryGetValue(ptr, out var obj) ? (T)obj : null;
        }

        public virtual void Dispose()
        {
            if (NativePtr == IntPtr.Zero) return;
            NativePtr = IntPtr.Zero;
        }

        protected bool CheckDisposedAndLogError(string message)
        {
            if (NativePtr != IntPtr.Zero) return false;
            Debug.LogErrorFormat("LevelPlay SDK: {0}: Instance of type {1} is disposed. Please create a new instance in order to call any method.", message, GetType().FullName);
            return true;
        }

    }
}

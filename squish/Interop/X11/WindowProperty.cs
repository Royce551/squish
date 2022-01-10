using System.Collections;
using TerraFX.Interop.Xlib;
using static TerraFX.Interop.Xlib.Xlib;

namespace Squish.Interop.X11;

public unsafe struct WindowProperty<TProperty> : IDisposable, IEnumerable<TProperty>
        where TProperty : unmanaged
{
    public WindowProperty(Atom type, int format, ulong itemCount, ulong remainingBytes, TProperty* data)
    {
        Type = type;
        Format = format;
        ItemCount = itemCount;
        RemainingBytes = remainingBytes;
        Data = data;
    }
    public Atom Type { get; }
    public int Format { get; }
    public ulong ItemCount { get; }
    public ulong RemainingBytes { get; }
    public TProperty* Data { get; }
    public void Dispose()
    {
        XFree(Data);
    }
    
    public TProperty this[int i]
    {
        get 
        {
            return data[i];
        }
    }
    
    public Enumerator GetEnumerator() => new Enumerator(this);
    
    IEnumerator<TProperty> IEnumerable<TProperty>.GetEnumerator() => new Enumerator(this);
    
    IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);
    public struct Enumerator : IEnumerator<TProperty>
    {
        WindowProperty<TProperty> windowProperty;
        ulong index = ulong.MaxValue;
        internal Enumerator(in WindowProperty<TProperty> windowProp)
        {
            windowProperty = windowProp;
        }
        public void Dispose()
        {
        }
        public bool MoveNext()
        {
            return index++ < windowProperty.ItemCount;
        }
        public void Reset()
        {
            index = ulong.MaxValue;
        }
        public TProperty Current => windowProperty[index];
        public object IEnumerator.Current => Current;
    }
}
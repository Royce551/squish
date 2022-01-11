using System.Collections;
using TerraFX.Interop.Xlib;
using static TerraFX.Interop.Xlib.Xlib;

namespace Squish.Interop.X11;

public struct WindowProperty<TProperty> : IDisposable, IEnumerable<TProperty>
        where TProperty : unmanaged
{
    public unsafe WindowProperty(Atom type, int format, ulong itemCount, ulong remainingBytes, TProperty* data)
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
    public unsafe TProperty* Data { get; }
    public unsafe void Dispose()
    {
        X11Exception.ThrowForErrorCode(
            XFree(Data)
        );
    }
    
    public unsafe TProperty this[int i] => Data[i];

    public Enumerator GetEnumerator() => new Enumerator(this);
    
    IEnumerator<TProperty> IEnumerable<TProperty>.GetEnumerator() => new Enumerator(this);
    
    IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);
    
    public struct Enumerator : IEnumerator<TProperty>
    {
        WindowProperty<TProperty> windowProperty;
        
        int index = -1;
        
        internal Enumerator(in WindowProperty<TProperty> windowProp)
        {
            windowProperty = windowProp;
        }
        
        public void Dispose()
        {
            
        }
        
        public bool MoveNext()
        {
            return index++ < (int) windowProperty.ItemCount - 1;
        }
        public void Reset()
        {
            index = -1;
        }
        
        public TProperty Current => windowProperty[index];
        
        object IEnumerator.Current => Current;
    }
}
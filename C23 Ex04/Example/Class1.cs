namespace Example;

public class Class1
{
    public static void Main()
    {
        A a1 = new A();
        A a2 = new A();

        a2.action += a1.foo;
        a2.print();
        
        a1.AttachSub(a2);
        a2.print();
    }

    public class A
    {
        public event Action<A> action;
        public void AttachSub(A a2)
        {
            a2.action += this.foo;
        }
        public void foo(A a){}
        public void print()
        {
            Console.WriteLine(action.GetInvocationList().Length);
        }
    }
}
namespace SplitExpensesCalculation.Models;

public class Member
{
    public Member(string name)
    {
        Name = name;
    }

    public string Name { get; init; }

    protected bool Equals(Member other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        //if (obj.GetType() != this.GetType()) return false;
        return Equals((Member)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
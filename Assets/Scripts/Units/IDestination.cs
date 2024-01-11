using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestination
{
    public void NavigateTo(IPathFollower pathFollower);
}

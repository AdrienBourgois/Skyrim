﻿public interface IUsableObject
{
    /// <summary>
    /// Function called when a character uses (Player pressed "Use" button) an IUsableObject.
    /// </summary>
    /// <param name="_character">The character that used the object.</param>
    void OnUse(ACharacter _character);
}

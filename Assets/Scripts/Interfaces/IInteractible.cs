using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible {
    void Start();
    void SetPreviousColor();
    void SetPreviousColor(GameObject gameObject);
    void ToggleHighlight();
    void Interact();
}

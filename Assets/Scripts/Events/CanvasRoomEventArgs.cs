using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType {
    UI, Video, Image, None
}

public class CanvasRoomEventArgs : EventArgs {
    public RoomType roomType;    
}

﻿using Fluent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversaTeste : FluentScript
{
    //public Text OtherTextElement;

    public override FluentNode Create()
    {
        return
            Show() *
            Options
            (
                Option("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typ") *
                    Hide() *
                    Yell("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typ") *
                    End()
             );
    }
}

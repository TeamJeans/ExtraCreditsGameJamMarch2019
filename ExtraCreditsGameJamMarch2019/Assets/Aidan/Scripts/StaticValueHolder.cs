﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticValueHolder {

	static int currentLevel = 1;
	public static int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
	
}
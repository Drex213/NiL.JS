// Copyright 2009 the Sputnik authors.  All rights reserved.
/**
 * String.prototype.indexOf(searchString, position)
 *
 * @path ch15/15.5/15.5.4/15.5.4.7/S15.5.4.7_A1_T2.js
 * @description Arguments are boolean equation, function and null, and instance is Boolean object
 */

var __instance = new Boolean;

__instance.indexOf = String.prototype.indexOf;

//////////////////////////////////////////////////////////////////////////////
//CHECK#1
if (__instance.indexOf("A"!=="\u0041", function(){return 0;}(),null) !== 0) {
  $ERROR('#1: __instance = new Boolean; __instance.indexOf = String.prototype.indexOf;  __instance.indexOf("A"!=="\\u0041", function(){return 0;}(),null) === 0. Actual: '+__instance.indexOf("A"!=="\u0041", function(){return 0;}(),null) ); 
}
//
//////////////////////////////////////////////////////////////////////////////

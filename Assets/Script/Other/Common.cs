using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Common
{
    /// <summary>
    /// 延遲幾秒後執行
    /// </summary>
    /// <param name="second"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IDisposable Timer(float second, Action callback)
    {
        return Observable.Timer(TimeSpan.FromSeconds(second)).DoOnCompleted(() =>
        {
            callback?.Invoke();
        }).Subscribe();
    }

    /// <summary>
    /// 隨機排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<T> RandomSort<T>(ref List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            SwapList<T>(ref list, 0, CheckValid_Int(UnityEngine.Random.Range(0, list.Count), 0, list.Count - 1));
        }
        return list;
    }

    /// <summary>
    /// 串列內容交換
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <returns></returns>
    public static List<T> SwapList<T>(ref List<T> list, int index1, int index2)
    {
        T temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
        return list;
    }

    /// <summary>
    /// 陣列內容交換
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <returns></returns>
    public static T[] SwapArray<T>(ref T[] array, int index1, int index2)
    {
        T temp = array[index1];
        array[index1] = array[index2];
        array[index2] = temp;
        return array;
    }

    /// <summary>
    /// 資料交換
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    public static void SwapData<T>(ref T t1, ref T t2)
    {
        T temp = t1;
        t1 = t2;
        t2 = temp;
    }

    /// <summary>
    /// 檢查範圍
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int CheckValid_Int(int num, int min, int max)
    {
        if (num < min)
            num = min;

        if (num > max)
            num = max;

        return num;
    }

    /// <summary>
    /// 檢查範圍
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float CheckValid_Float(float num, float min, float max)
    {
        if (num < min)
            num = min;

        if (num > max)
            num = max;

        return num;
    }

    /// <summary>
    /// 數值在範圍內(含等於)
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static bool InRangeEqual(float num, float min, float max)
    {
        return num >= min && num <= max;
    }

    /// <summary>
    /// 數值在範圍內(不含等於)
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static bool InRangeNoEqual(float num, float min, float max)
    {
        return num > min && num < max;
    }
}
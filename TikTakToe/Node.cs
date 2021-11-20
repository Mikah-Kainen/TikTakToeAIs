﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TikTakToe
{
    public class Node<T> where T : IGameState<T>
    {
        public T State { get; set; }
        public int Value { get; set; }
        public List<Node<T>> Children { get; set; }
        public Node()
        {
        }

        public void CreateTree(T startingState)
        {
            State = startingState; 
            if(startingState == null)
            {
                throw new Exception("Null startingState");
            }

            BuildTree();
            SetValues(Players.Player1, Players.Player2);
        }

        private void BuildTree()
        {
            Children = new List<Node<T>>();
            T[] values = State.GetChildren();
            if (!State.IsTerminal)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    Node<T> Child = new Node<T>();
                    Child.State = values[i];
                    Child.BuildTree();
                    Children.Add(Child);
                }
            }
        }

        private int SetValues(Players maximizer, Players minimizer)
        {
            if (State.IsTerminal)
            {
                Players winner = State.GetWinner();
                if (winner == maximizer)
                {
                    return 1;
                }
                else if (winner == minimizer)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            int[] values = new int[Children.Count];
            int smallestValue = int.MaxValue;
            int largestValue = int.MinValue;
            for (int i = 0; i < Children.Count; i++)
            {
                values[i] = Children[i].SetValues(maximizer, minimizer);
                if (values[i] < smallestValue)
                {
                    smallestValue = values[i];
                }
                if (values[i] > largestValue)
                {
                    largestValue = values[i];
                }
            }
            if (State.NextPlayer == maximizer)
            {
                Value = largestValue;
                return largestValue;
            }
            else
            {
                Value = smallestValue;
                return smallestValue;
            }
        }

        //public void SetChildren(T[] values)
        //{
        //    Children = new List<Node<T>>();
        //    for (int i = 0; i < values.Length; i ++)
        //    {
        //        Node<T> child = new Node<T>();
        //        child.Value = values[i];
        //        Children.Add(child);
        //    }
        //}
    }
}

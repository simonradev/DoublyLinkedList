namespace DoublyLinkedList
{
    using System;

    /// <summary>
    /// With the list you can collect and organize elements and data.
    /// This is a dynamic implementation, which means that adding works fast but indexes dont.
    /// </summary>
    /// <typeparam name="TType">The type of elements you want to collect</typeparam>
    public class DoubleLinkedList<TType>
    {
        /// <summary>
        /// A single element from the list which holds a reference to the next and previous element.
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Holds the reference of the next Node.
            /// </summary>
            private Node next;
            /// <summary>
            /// Holds the reference of the previous Node.
            /// </summary>
            private Node previous;
            /// <summary>
            /// Holds the value of the current Node.
            /// </summary>
            private TType value;

            /// <summary>
            /// Enables you you to set or get the next Node.
            /// </summary>
            public Node Next
            {
                get { return this.next; }
                set { this.next = value; }
            }

            /// <summary>
            /// Enables you to get or set the previous Node.
            /// </summary>
            public Node Previous
            {
                get { return this.previous; }
                set { this.previous = value; }
            }

            /// <summary>
            /// Enables you to get or set the value of the current Node.
            /// </summary>
            public TType Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        /// <summary>
        /// Holds the first element of the collection and a reference to the next ones.
        /// </summary>
        private Node head;
        /// <summary>
        /// Holds the last element of the collection and a reference to the previous ones.
        /// </summary>
        private Node tail;
        /// <summary>
        /// Holds the number of elements which are currently in the list.
        /// </summary>
        private int count;

        /// <summary>
        /// Sets the fields to their default values.
        /// </summary>
        public DoubleLinkedList()
        {
            this.count = 0;
            this.head = default(Node);
            this.tail = default(Node);
        }

        /// <summary>
        /// Holds the count of the elements in the list.
        /// </summary>
        public int Count
        {
            get { return this.count; }
        }

        /// <summary>
        /// Enables the lists elements to be accesed by index.
        /// </summary>
        /// <param name="index">The index of the element you want.</param>
        /// <returns>The element at the index entered.</returns>
        public TType this[int index]
        {
            get { return GetElementByIndex(index); }
            set { AddElementToIndexOrOverrideExcistingElement(value, index); }
        }

        /// <summary>
        /// This is a method written to be used only from the proporty which enables indexing the list.
        /// </summary>
        /// <param name="elementToAdd">Adds or overwrites and element at an index.</param>
        /// <param name="indexToAddIn">The index of the element you want to update or create.</param>
        private void AddElementToIndexOrOverrideExcistingElement(TType elementToAdd, int indexToAddIn)
        {
            if (indexToAddIn < 0 || indexToAddIn > count)
            {
                throw new InvalidOperationException("The index is out of the bounds of the list!!!");
            }

            if (count == 0)
            {
                head = new Node();
                head.Next = default(Node);
                head.Previous = default(Node);
                head.Value = elementToAdd;

                tail = head;
            }
            else if (indexToAddIn == 0)
            {
                head.Value = elementToAdd;

                count--;
            }
            else if (indexToAddIn + 1 == count)
            {
                tail.Value = elementToAdd;

                count--;
            }
            else
            {
                Node currNode = head;
                int currIndex = 1;
                while (currIndex != indexToAddIn)
                {
                    currNode = currNode.Next;

                    currIndex++;
                }

                try
                {
                    currNode.Next.Value = elementToAdd;

                    count--;
                }
                catch (NullReferenceException)
                {
                    Node toAdd = new Node();
                    toAdd.Value = elementToAdd;

                    currNode.Next = toAdd;
                    toAdd.Previous = currNode;

                    tail = toAdd;
                }
            }

            count++;
        }

        /// <summary>
        /// Adds and element at the end of the list.
        /// </summary>
        /// <param name="elementToAdd">The element you want to add at the end.</param>
        public void AddInTheEnd(TType elementToAdd)
        {
            if (head == default(Node))
            {
                head = new Node();
                head.Previous = default(Node);
                head.Next = default(Node);
                head.Value = elementToAdd;

                tail = head;
            }
            else
            {
                Node toAdd = new Node();
                toAdd.Value = elementToAdd;

                Node currNode = head;
                while (currNode.Next != default(Node))
                {
                    currNode = currNode.Next;
                }

                currNode.Next = toAdd;
                toAdd.Previous = currNode;

                tail = toAdd;
            }

            count++;
        }

        /// <summary>
        /// Adds an element at the beginning of the list.
        /// Be careful with the order the elements where put in it may not be what you want.
        /// </summary>
        /// <param name="elementToAdd">The element you want to add in the beginning.</param>
        public void AddInTheBeginning(TType elementToAdd)
        {
            if (head == default(Node))
            {
                head = new Node();
                head.Previous = default(Node);
                head.Next = default(Node);
                head.Value = elementToAdd;

                tail = head;
            }
            else
            {
                Node toAdd = new Node();
                toAdd.Value = elementToAdd;
                toAdd.Next = head;

                head.Previous = toAdd;
                head = toAdd;

                Node currNode = head;
                while (currNode.Next != default(Node))
                {
                    currNode = currNode.Next;
                }

                tail = currNode;
            }

            count++;
        }

        /// <summary>
        /// Adds an element at an index.
        /// If you add at index [0] the element goes before the current element at index [0].
        /// </summary>
        /// <param name="elementToAdd">The element you want to add.</param>
        /// <param name="indexToAddAt">The index you want to add the element in.</param>
        public void AddAtIndex(TType elementToAdd, int indexToAddAt)
        {
            if (indexToAddAt < 0 || indexToAddAt > count)
            {
                throw new InvalidOperationException(
                    "You can't add at this position. It is out of the boundaries of the list!!!");
            }

            Node toAdd = new Node();
            toAdd.Value = elementToAdd;

            if (count == 0)
            {
                head = new Node();
                head.Value = elementToAdd;
                head.Next = default(Node);
                head.Previous = default(Node);

                tail = head;
            }
            else if (indexToAddAt == 0)
            {
                head.Previous = toAdd;
                toAdd.Next = head;
                toAdd.Previous = default(Node);

                head = toAdd;
            }
            else if (indexToAddAt == count)
            {
                tail.Next = toAdd;
                toAdd.Previous = tail;
                toAdd.Next = default(Node);

                tail = toAdd;
            }
            else
            {
                Node currNode = head;
                int currIndex = 1;
                while (currIndex != indexToAddAt)
                {
                    currNode = currNode.Next;

                    currIndex++;
                }

                Node nextNode = currNode.Next;

                toAdd.Next = nextNode;
                toAdd.Previous = currNode;

                currNode.Next = toAdd;
                nextNode.Previous = toAdd;
            }

            count++;
        }

        /// <summary>
        /// Adds a collection of elements in the index entered.
        /// </summary>
        /// <param name="index">The index where you want to insert the collection.</param>
        /// <param name="elementsToAdd">The collection you want to insert in the index.</param>
        public void AddRange(int index, params TType[] elementsToAdd)
        {
            if (index < 0 || index > count)
            {
                throw new InvalidOperationException(
                       "You can't add at this position. It is out of the boundaries of the list!!!");
            }

            for (int currElement = 0; currElement < elementsToAdd.Length; currElement++)
            {
                TType currElementToAdd = elementsToAdd[currElement];

                AddAtIndex(currElementToAdd, index);

                index++;
            }
        }

        /// <summary>
        /// Checks if an element excists in the list or not.
        /// </summary>
        /// <param name="elementToCheck">The element you want to check.</param>
        /// <returns>"True" if the seached element exists and "False" if it does not.</returns>
        public bool Contains(TType elementToCheck)
        {
            bool containsItem = false;

            if (head.Value.Equals(elementToCheck))
            {
                containsItem = true;

                return containsItem;
            }
            else if (tail.Value.Equals(elementToCheck))
            {
                containsItem = true;

                return containsItem;
            }
            else
            {
                Node currNode = head.Next;
                while (!currNode.Equals(tail))
                {
                    if (currNode.Value.Equals(elementToCheck))
                    {
                        containsItem = true;

                        return containsItem;
                    }

                    currNode = currNode.Next;
                }
            }

            return containsItem;
        }

        /// <summary>
        /// Removes the first occurrency of an element.
        /// </summary>
        /// <param name="elementToRemove">The element you want to remove.</param>
        public void RemoveFirst(TType elementToRemove)
        {
            if (count == 0)
            {
                throw new InvalidOperationException("The list is empty!!! Try adding something first!!!");
            }

            Node currNode = head;
            if (!currNode.Value.Equals(elementToRemove))
            {
                try
                {
                    while (!currNode.Next.Value.Equals(elementToRemove))
                    {
                        currNode = currNode.Next;
                    }
                }
                catch (NullReferenceException)
                {
                    throw new InvalidOperationException("No such element excists!!!");
                }

                try
                {
                    Node prevNode = currNode;
                    Node nextNode = currNode.Next.Next;
                    prevNode.Next = nextNode;
                    nextNode.Previous = prevNode;
                }
                catch (NullReferenceException)
                {
                    currNode.Next = default(Node);
                    tail = currNode;
                }
            }
            else
            {
                head = head.Next;
                head.Previous = default(Node);
            }

            count--;
        }

        /// <summary>
        /// Removes the last occurrency of an element.
        /// </summary>
        /// <param name="elementToRemove">The element you want to remove.</param>
        public void RemoveLast(TType elementToRemove)
        {
            if (count == 0)
            {
                throw new InvalidOperationException("The list is empty!!! Try adding something first!!!");
            }

            Node currNode = tail;
            if (!currNode.Value.Equals(elementToRemove))
            {
                try
                {
                    while (!currNode.Previous.Value.Equals(elementToRemove))
                    {
                        currNode = currNode.Previous;
                    }
                }
                catch (NullReferenceException)
                {
                    throw new InvalidOperationException("No such element excists!!!");
                }

                try
                {
                    Node nextNode = currNode;
                    Node prevNode = currNode.Previous.Previous;
                    nextNode.Previous = prevNode;
                    prevNode.Next = nextNode;
                }
                catch (NullReferenceException)
                {
                    head = head.Next;
                    head.Previous = default(Node);
                }
            }
            else
            {
                tail = tail.Previous;
                tail.Next = default(Node);
            }

            count--;
        }

        /// <summary>
        /// Removes all elements you want.
        /// The operation starts from the beginning of the list.
        /// </summary>
        /// <param name="elementToRemove">The elements you want to remove.</param>
        public void RemoveAll(TType elementToRemove)
        {
            if (count == 0)
            {
                throw new InvalidOperationException("The list is empty!!! Try adding something first!!!");
            }

            bool allElementsAreRemoved = false;
            while (!allElementsAreRemoved)
            {
                try
                {
                    RemoveFirst(elementToRemove);
                }
                catch (InvalidOperationException)
                {
                    allElementsAreRemoved = true;
                }
            }
        }

        /// <summary>
        /// Removes an element at the entered index.
        /// </summary>
        /// <param name="indexToRemoveAt">The index of the element you want to remove.</param>
        public void RemoveAt(int indexToRemoveAt)
        {
            if (count == 0)
            {
                throw new InvalidOperationException("The list is empty!!! Try adding something first!!!");
            }

            if (indexToRemoveAt == 0)
            {
                head = head.Next;
                head.Previous = default(Node);
            }
            else if ((indexToRemoveAt + 1) == count)
            {
                tail = tail.Previous;
                tail.Next = default(Node);
            }
            else
            {
                Node currNode = head;
                int currIndex = 0;
                while (currIndex != indexToRemoveAt)
                {
                    currNode = currNode.Next;
                    currIndex++;
                }

                Node prevNode = currNode.Previous;
                Node nextNode = currNode.Next;

                prevNode.Next = nextNode;
                nextNode.Previous = prevNode;
            }

            count--;
        }

        /// <summary>
        /// Searches the element at the entered index.
        /// </summary>
        /// <param name="indexToGetFrom">The index of the element you want to get.</param>
        /// <returns>Returns the element from the entered index in the collection.</returns>
        public TType GetElementByIndex(int indexToGetFrom)
        {
            if (indexToGetFrom < 0 || indexToGetFrom >= count)
            {
                throw new InvalidOperationException("The index is outside the bounds of the list!!!");
            }

            TType elementToReturn = default(TType);

            if (indexToGetFrom == 0)
            {
                elementToReturn = head.Value;
            }
            else if (indexToGetFrom + 1 == count)
            {
                elementToReturn = tail.Value;
            }
            else
            {
                Node currNode = head.Next;
                int currIndex = 1;
                while (currIndex != indexToGetFrom)
                {
                    currNode = currNode.Next;

                    currIndex++;
                }

                elementToReturn = currNode.Value;
            }

            return elementToReturn;
        }

        /// <summary>
        /// Returns the index of the first occurrency of the element you want.
        /// </summary>
        /// <param name="elementToCheck">Enter the element which index you want to know.</param>
        /// <returns>Returns the index of the element you want.</returns>
        public int GetIndex(TType elementToCheck)
        {
            if (!Contains(elementToCheck))
            {
                throw new InvalidOperationException("No such element excists in the list!!!");
            }

            int indexOfElement = 0;
            Node currNode = head;
            while (!currNode.Equals(tail) && !currNode.Value.Equals(elementToCheck))
            {
                currNode = currNode.Next;

                indexOfElement++;
            }

            return indexOfElement;
        }

        /// <summary>
        /// Transfers the elements from the list to an array.
        /// </summary>
        /// <returns>An array collection with all elements from the list.</returns>
        public TType[] ToArray()
        {
            TType[] arrayToReturn = new TType[count];

            for (int currElement = 0; currElement < count; currElement++)
            {
                arrayToReturn[currElement] = GetElementByIndex(currElement);
            }

            return arrayToReturn;
        }

        /// <summary>
        /// Joins the data in the following way [1, 2, 3...n-1]
        /// </summary>
        /// <returns>Returns all elements separated by a comma.</returns>
        public override string ToString()
        {
            return string.Join(", ", ToArray());
        }
    }
}

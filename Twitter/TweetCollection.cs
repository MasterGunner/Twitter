using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter
{
    public class TweetCollection : ICollection<Tweet>
    {
        // The generic enumerator obtained from IEnumerator<Tweet>
        // by GetEnumerator can also be used with the non-generic IEnumerator.
        // To avoid a naming conflict, the non-generic IEnumerable method
        // is explicitly implemented.

        public IEnumerator<Tweet> GetEnumerator()
        {
            return new TweetEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TweetEnumerator(this);
        }

        // The inner collection to store objects.
        private List<Tweet> innerCol;

        // For IsReadOnly
        private bool isRO = false;

        public TweetCollection()
        {
            innerCol = new List<Tweet>();
        }

        // Adds an index to the collection.
        public Tweet this[int index]
        {
            get { return (Tweet)innerCol[index]; }
            set { innerCol[index] = value; }
        }

        // Determines if an item is in the collection
        // by using the SameTweet equality comparer.
        public bool Contains(Tweet item)
        {
            bool found = false;

            foreach (Tweet tw in innerCol)
            {
                // Equality defined by the Tweet
                // class's implmentation of IEquitable<T>.
                if (tw.Equals(item))
                {
                    found = true;
                }
            }

            return found;
        }

        // Determines if an item is in the 
        // collection by using a specified equality comparer.
        public bool Contains(Tweet item, EqualityComparer<Tweet> comp)
        {
            bool found = false;

            foreach (Tweet tw in innerCol)
            {
                if (comp.Equals(tw, item))
                {
                    found = true;
                }
            }

            return found;
        }

        // Adds an item if it is not already in the collection
        // as determined by calling the Contains method.
        public void Add(Tweet item)
        {
            innerCol.Add(item);
        }

        public void Clear()
        {
            innerCol.Clear();
        }

        public void CopyTo(Tweet[] array, int arrayIndex)
        {
            for (int i = 0; i < innerCol.Count; i++)
            {

                array[i] = (Tweet)innerCol[i];
            }
        }

        public int Count
        {
            get
            {
                return innerCol.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return isRO; }
        }

        public bool Remove(Tweet item)
        {
            bool result = false;

            // Iterate the inner collection to 
            // find the Tweet to be removed.
            for (int i = 0; i < innerCol.Count; i++)
            {

                Tweet curTweet = (Tweet)innerCol[i];

                if (new SameTweet().Equals(curTweet, item))
                {
                    innerCol.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
        }
    }

    // Defines the enumerator for the Tweet collection.
    // (Some prefer this class nested in the collection class.)
    public class TweetEnumerator : IEnumerator<Tweet>
    {
        private TweetCollection _collection;
        private int curIndex;
        private Tweet curTweet;


        public TweetEnumerator(TweetCollection collection)
        {
            _collection = collection;
            curIndex = -1;
            curTweet = default(Tweet);

        }

        public bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++curIndex >= _collection.Count)
            {
                return false;
            }
            else
            {
                // Set current Tweet to next item in collection.
                curTweet = _collection[curIndex];
            }
            return true;
        }

        public void Reset() { curIndex = -1; }

        void IDisposable.Dispose() { }

        public Tweet Current
        {
            get { return curTweet; }
        }


        object IEnumerator.Current
        {
            get { return Current; }
        }

    }
}

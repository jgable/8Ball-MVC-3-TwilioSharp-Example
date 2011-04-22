using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace _8Ball.Common
{
    /// <summary>
    /// A Magical Mother Effin 8 Ball
    /// </summary>
    public class Magic8BallAnswerizer3000
    {
        /// <summary>
        /// A Thread-Safe Random.
        /// </summary>
        protected static ThreadLocal<Random> _random = new ThreadLocal<Random>(valueFactory: () => new Random());

        /// <summary>
        /// The collection of 8-Ball Answers.
        /// </summary>
        protected static ReadOnlyCollection<string> _answers = new ReadOnlyCollection<string>(new List<string>() 
        {
            "It is certain",
            "It is decidedly so",
            "Without a doubt",
            "Yes, definitely",
            "You may rely on it",
            "As I see it, yes",
            "Most likely",
            "Outlook good",
            "Signs point to yes",
            "Yes",
            "Reply hazy, try again",
            "Ask again later",
            "Better not tell you now",
            "Cannot predict now",
            "Concentrate and ask again",
            "Don't count on it",
            "My reply is no",
            "My sources say no",
            "Outlook not so good",
            "Very doubtful",
        });

        /// <summary>
        /// Gets an answer.
        /// </summary>
        /// <returns>An answer from the magic 8 ball.</returns>
        public static string GetAnswer()
        {
            return _answers[_random.Value.Next(_answers.Count)];
        }
    }
}
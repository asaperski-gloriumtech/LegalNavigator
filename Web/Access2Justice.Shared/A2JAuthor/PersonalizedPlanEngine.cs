﻿using Access2Justice.Shared.Extensions;
using Access2Justice.Shared.Interfaces;
using Access2Justice.Shared.Interfaces.A2JAuthor;
using Access2Justice.Shared.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Access2Justice.Shared.A2JAuthor
{
    public class A2JAuthorPersonalizedPlanEngine : IPersonalizedPlanEngine
    {
        private readonly IPersonalizedPlanParse parser;

        public A2JAuthorPersonalizedPlanEngine(IPersonalizedPlanParse parser)
        {
            this.parser = parser;
        }

        public UnprocessedPersonalizedPlan Build(JObject personalizedPlan, CuratedExperienceAnswers userAnswers)
        {
            var stepsInScope = new List<JToken>();
            var evaluatedUserAnswers = parser.Parse(userAnswers);

            var root = personalizedPlan
                .Properties()
                .GetArrayValue("rootNode")
                .FirstOrDefault();

            foreach (var child in root.GetValueAsArray<JArray>("children"))
            {
                var states = child.GetArrayValue("state");
                foreach (var state in states)
                {
                    foreach (var answer in evaluatedUserAnswers)
                    {
                        if (answer.Key == state.GetValue("leftOperand") && answer.Value == state.GetValue("operator"))
                        {
                            stepsInScope.Add(child);
                        }
                    }
                }
            }

            var unprocessedPlan = new UnprocessedPersonalizedPlan();
            unprocessedPlan.Id = Guid.NewGuid();

            foreach (var step in stepsInScope)
            {
                foreach (var childrenRoot in step.GetValueAsArray<JArray>("children"))
                {
                    ////var tempchild2 = childrenRoot.GetValueAsArray<JObject>("rootNode").GetArrayValue("children");
                    //var tempchild = childrenRoot.GetValueAsArray<JObject>("rootNode").GetValueAsArray<JArray>("children").FirstOrDefault();
                    //var tempstate = tempchild.GetArrayValue("state").FirstOrDefault();

                    ////var Title = tempstate.GetValue("title");
                    ////var Description = tempstate.GetValue("userContent");
                    

                    var childern = childrenRoot.GetValueAsArray<JObject>("rootNode").GetValueAsArray<JArray>("children");
                    var topic = new UnprocessedTopic();

                    foreach (var child in childern)
                    {
                        
                        var state = child.GetArrayValue("state").FirstOrDefault();
                        topic.Id = Guid.NewGuid();

                        if (!string.IsNullOrWhiteSpace(state.GetValue("title")))
                        {
                            topic.Title = state.GetValue("title");
                            continue;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(state.GetValue("userContent")))
                            {
                                topic.Description = state.GetValue("userContent");
                            }
                        }

                        topic.ResourceIds = ExtractResourceIds(state.GetValue("userContent"));
                        unprocessedPlan.UnprocessedTopics.Add(topic);
                    }
                }
            }

            return unprocessedPlan;
        }

        private List<Guid> ExtractResourceIds(string v)
        {
             // Todo:@Alaa implement this
            return null;
        }
    }
}

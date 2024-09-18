# Software Developer Technical Test | SCRM Lidl International Hub

## Table of Contents
- [Software Developer Technical Test | SCRM Lidl International Hub](#software-developer-technical-test--scrm-lidl-international-hub)
  - [Table of Contents](#table-of-contents)
  - [Overview](#overview)
  - [About the test](#about-the-test)
  - [Test Requirements](#test-requirements)
  - [Bonus](#bonus)
  - [Submission Guidelines](#submission-guidelines)

## Overview
Welcome to the technical test for the Software Developer position at SCRM Lidl International Hub for Promotion Engine team. This test is designed to evaluate your coding skills, problem-solving abilities, and understanding of software development principles.

## About the test
Please read the instructions carefully before starting the test. Ensure you understand the requirements and guidelines.
The test will consist in 4 parts:

1. Fixing technical problems (which we introduced) and refactoring
2. Implementing a new endpoint
3. Unit testing
4. Explain and justify your decisions in a file

> [!WARNING]
> This test is proposed to evaluate your performance and knowledge on tipical daily tasks in our development environment. Focus on code and in the task. Any consideration you would like to add to architecture, repo design, etc ... add it in your final explanations.

## Test Requirements
You will be required to complete the following tasks:

> [!TIP]
> You can use any library you'd like to use, but! you will have to explain why did you add it and which benefits it brings to the project.

1. **Task 1: Fix and refactor /v1/{countryCode}/promotions endpoint**
In the features folder you will find a working endpoint, 

   - Make any code changes you need to solve technical problems (do not focus on business issues since this is not a real business case and has no real requirements, this is just an example)
   - Optimize anything you see it can be optimized performance wise
   - Only business requirement is that the endpoint has to return a json like this : 
  
   ```json
   {
      "promotions": [
         {
            "promotionId": "d2192809-33ee-48be-ad0e-88d660edb531",
            "texts": {
               "title": "Title",
               "description": "Description",
               "discountTitle": "Discount Title",
               "discountDescription": "Discount Description"
            },
            "endValidityDate": "2024-07-17T00:00:00Z",
            "images": [
               "Image1",
               "Image2"
            ],
            "discounts": [
               {
                  "type": "Store",
                  "originalPrice": 1,
                  "finalPrice": 1,
                  "lowestPriceLast30Days": 1,
                  "priceType": "Type1",
                  "unitsToBuy": 1,
                  "unitsToPay": 1,
                  "hasPrice": true
               }
            ]
         }
      ]
   }
   ```

2. **Task 2: Implement new endpoint version**
Implement a new endpoint version of **/v1/{countryCode}/promotions** include the following changes

  - Add a new parameter "maxPromotions" to the controller coming from the **query string**
  - Limit the number of promotions returned using this parameter
  - Add a new field in the response model called "promotionCount" which has to match the number of elements returned 

3. **Task 3: Implement new endpoint**
Create a simple endpoint to return instead of a list of promotions, a single promotion

   - The new endpoint should return a single document with the same model than the getAll endpoint filtering by id.
   - Structure should be consistent with the already implemented endpoints in the API. 
   - Apart from that you are free to implement this freely. 

4. **Task 4: Write Unit Tests**
Write at least the most meaningful unit test for your new endpoint. Explain why did you consider this the most important unit test.

## Bonus
Implement any other test you think could be necessary for the api.
Write a dockerfile for this api and provide instructions to run the container.
Add model validation you might find useful at controller level

## Submission Guidelines
1. Ensure your code is well-documented and follows best practices.
2. Include a `README_explanation.md` file explaning the reason behind your proposed changes and your implementation, any details are appreciated as this will provide us insights about your process while doing the test.
3. Email us with the results in a zip file or with a public repository url

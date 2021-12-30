# Acebook API Engineering Project (C# + ASP.NET Core)

Software engineers nearly always work in teams. In this module you will work in
a simulated engineering team to develop the backend of a social media
application. You will implement tickets on an existing system. You will
encounter authentic problems, and use authentic practices and tools to resolve
them.

This module is tailored to be particularly relevant to roles that are primarily
back-end software engineering using C# with ASP.NET Core and deploying to Azure.

## Learning Objective

This week your learning objective is to learn to:

* Make production-quality changes
* To a realistic software system
* As a professional software engineering team.

This objective is important because it is exactly what your job will involve.

### How will we know if our changes are production-quality?

Organisations want to deliver great software to users in a timely fashion.
Production-quality software is software that enables this. For example:

* **Testing.**  
  Tests help by making it easier to verify nothing has broken when you make
  changes. Untested code hinders this by making it easy to break things,
  resulting in going back over things and rework.

* **Readable code.**  
  Readability helps this by making it easier for developers to understand what
  is happening in the code, so that they can then change it. Hard-to-understand
  code means regularly having to ask your colleagues to unpick what they've
  written so that you can understand it, slowing down the delivery of the
  software.

* **Single Responsibility Principle.**  
  Modules following the single responsibility principle make it easier for
  developers to define and isolate the change to be made in a way that doesn't
  break anything else. Large, complex code modules are slow to change and hard
  to verify, slowing down the team and the software delivery.

So you will know if your software is production-quality if, at the end of the
two weeks, you find it pleasant, easy, and quick to make changes to.

## What is a realistic software system?

Real software systems are different to the systems you've developed so far.
The engineering skills you have been developing will help you meet these 
challenges.

* **Complexity.**  
  Real software systems have a lots of different pieces working together. A lot
  of engineering is building a mental picture of how the pieces interact in
  different situations. Diagrams will help you here.

* **The Unknown.**  
  In the real world, software engineers regularly encounter a great many things
  they don't know and have to figure out, even in the absence of guidance and
  documentation. Considered reading, investigation, and experimentation will 
  help you here.

* **Deployment.**  
  Software is deployed to real servers accessible to real users. The software
  should work in that context, not just on your computer. To achieve this,
  you'll need to understand the deployment environment and create software with
  that in mind.

Realistic software development may be much slower than you expect, and you may
spend less time writing code than in previous modules.

### How will we know if we're working well in a team?

You will know your team is working well if:

* **You're shipping features.**  
  Software, deployed, getting into the hands of the users — that is the aim of
  your work! But beware: it's easy to start off fast, but staying fast is the
  real challenge (and often involves starting slow).

* **Everyone is contributing.**  
  No one should be sitting around waiting, no one should be passively letting
  the team get on with things. If this happens, you are wasting energy that can
  be going into building software.

* **You feel good.**  
  That means you're well-rested, taking care of yourselves, speaking and
  listening to each other. Treat frustration like thirst — not a problem to
  suppress, but a helpful signal from yourself to surface and figure out how to
  fix. Don't stew!

* **You're learning as you go.**  
  As software engineers you will be learning for your whole career. If you try
  to learn everything up-front you will find there's always more you don't know.
  As engineers, you should identify your task first, then what you need to learn
  to get started. Then get started and learn as you go.

* **You're proud of your work.**  
  If you know you're working together effectively, if you know you're building
  production-quality software, if you know you're learning and taking care of
  yourselves, you should be proud.

## Beginner Team Process

Software teams are organised in many ways, all of them united by the aim to
create quality software in a timely fashion. Here's a guide for you as
beginners:

### Team Setup

1. **Name your team.**  
   Choose something you all like (Coldplay?), and name your team after that!

2. **Agree your working practices.**  
   It is much easier to agree up front than find out you have differing views
   half way through. Consider agreeing and writing down these points:

   * When will you start and finish working for the day?
   * How often will you take breaks?
   * Your cycle of stand-ups and retrospectives?  
     Suggestion: stand-ups at the start of each day, and short retrospectives at
     the end of each day.
   * If someone feels frustrated or hopes for a change, how should they raise it
     with the team?
   * Anything else that has been an issue in past teams.

3. **Create a team Slack channel.**  
   Put links to your card board & Github repo in the title, and invite your
   coaches for this module.

4. **Get yourself a hot drink!**  
   That's the setup done.

### Planning

Software projects need replanning on a regular basis as things change. You will
do this process multiple times.

As a team:

1. **Review the Project board.**  
   Get a general update on what's changed since the last planning session. What
   have you done? What work is still in progress? Is anything blocked?

2. **Decide what to do next.**  
   How can you arrange the work so that everyone has something to do? Can you
   avoid working on the same areas of the codebase? If not, how will you
   coordinate?

3. **Estimate the work.**  
   Pick one of the tickets you've decided to do, and determine collectively how
   long you think it will take. Engineers often use t-shirt sizing (XS, S, M, L,
   XL) rather than time, to capture how 'complex' a task is.

   To do this, you'll need to think in a bit of detail what you'll need to do to
   achieve it. You might need to make some design decisions — note these on the
   card. You might find the ticket is better be split up into smaller tickets —
   feel free to do so.

4. **Allocate the tickets to pairs.**  
   Again, try to ensure you're maximising the contribution of everyone in the
   team.

5. **Get going!**  
   Take a break beforehand if you feel best.

### Coding

You'll be reasonably familiar with this bit, but there are some new
collaborative steps too.

Each pair should:

1. **Assign themselves to the card and move it to 'In Progress'.**  
   In a real development team, this would make sure no one else takes it on!

2. **Create a branch for the change.**  
   Call it something relevant to the feature.

3. **Implement the change.**  
   This will involve pairing and programming, but it might also involve taking a
   bit of time out to research. As always, when pairing, make sure you take
   breaks, and make sure you're spending an equal amount of time driving and
   navigating.

4. **Create a pull request.**  
   When you're satisfied your work is production quality, create a pull request
   and ask the other team to review it. Make sure to respond to requests for
   review in a timely fashion to avoid wasted time.

5. **Respond to the review.**  
   Is your code perfect? Very unlikely at this point in your career! A good code
   review will involve comments and those comments should lead you to improve
   your code, so do that.

6. **When your PR is approved, merge your code into the main branch.**  
   After this, the tests will run and the code will deploy to the server. This
   is called Continuous Integration & Deployment (CI-CD), and it's a marker of
   high quality in an engineering team, as the value gets to the users quickly.

7. **Verify the deployment has worked correctly.**
   In the real world, you'll have a staging server to check things on before
   sending it live to users. For now, check for yourself it works correctly and
   then move the card into 'Done'
   
Then it's back to the start! If you've not agreed as a team what to do next,
then it's time for another planning session.

### Retrospection

Here are some points to consider at your retrospectives:

* How much fun did you have today? 1-10
* What have we implemented? Demo for each other!
* Are we wasting time anywhere?
* Do we have any quality concerns to address?
* Were our estimates accurate? What did we get forget about?

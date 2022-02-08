using Bookist.Entities;

namespace Migrator;

public class MockData
{
    private readonly MainDbContext _context;

    public MockData(MainDbContext context)
    {
        _context = context;
    }

    public void Execute()
    {
        if (_context.Set<Book>().Any())
            return;

        var tag = new Tag { Id = 1001, Name = "DepOps" };
        _context.Add(tag);

        for (var i = 0; i < 100; i++)
        {
            _context.Set<Book>().Add(new Book
            {
                Id = 1000 + i,
                Author = "Jez Humble, David Farley",
                Cover = "2022/02/Fp2VO_f5Tu3QjgRWYv-5DqcTuadO.jpg",
                FetchUrl = "https://545c.com/file/15677019-227883293",
                Formats = "PDF",
                Intro = @"<h3>简介</h3>
<p>
  Getting software released to users is often a painful, risky, and time-consuming process. This
  groundbreaking new book sets out the principles and technical practices that enable rapid,
  incremental delivery of high quality, valuable new functionality to users. Through automation of
  the build, deployment, and testing process, and improved collaboration between developers,
  testers, and operations, delivery teams can get changes released in a matter of hours— sometimes
  even minutes–no matter what the size of a project or the complexity of its code base.
</p>
<p>
  Jez Humble and David Farley begin by presenting the foundations of a rapid, reliable, low-risk
  delivery process. Next, they introduce the “deployment pipeline,” an automated process for
  managing all changes, from check-in to release. Finally, they discuss the “ecosystem” needed to
  support continuous delivery, from infrastructure, data and configuration management to governance.
</p>
<h3>目录</h3>
<p>
  Foreword by Martin Fowler<br />Preface<br />Acknowledgements<br />About the Authors<br />Part I
  Foundations<br />1 The Problem of Delivering Software<br />2 Configuration Management<br />3
  Continuous Integration<br />4 Implementing a Testing Strategy<br />Part II The Deployment
  Pipeline<br />5 Anatomy of the Deployment Pipeline<br />6 Build and deployment scripting<br />7
  Commit Testing Stage<br />8 Automated Acceptance Testing<br />9 Testing Non-Functional
  Requirements<br />10 Deploying and Releasing Applications<br />Part III The Delivery Ecosystem<br />11
  Managing infrastructure and environments<br />12 Managing Data<br />13 Managing components and
  dependencies<br />14 Advanced version control<br />15 Managing Continuous Delivery<br />Bibliography<br />Index
</p>",
                PubDate = new DateTime(2010, 7, 1),
                OrgUrl = "https://www.amazon.com/Continuous-Delivery-Deployment-Automation-Addison-Wesley-dp-0321601912/dp/0321601912",
                Subtitle = "Reliable Software Releases through Build, Test, and Deployment Automation",
                Title = "Continuous Delivery " + (1000 + i),
                Tags = new List<Tag> { tag }
            });
        }

        _context.SaveChanges();
    }
}
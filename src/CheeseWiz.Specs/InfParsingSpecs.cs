using CheeseWiz.InfModel;
using CheeseWiz.InfParsing;
using CheeseWiz.Logging;
using CheeseWiz.Specs.BDD;
using NUnit.Framework;

namespace CheeseWiz.Specs
{
	public class InfParsingSpecs
	{
		public static class under
		{
			public abstract class the_default_context : ContextSpecification
			{
				protected InfParser SUT;
				protected Inf inf;

				protected override void EstablishContext()
				{
					ILogger logger = Mock<ILogger>();
					SUT = new InfParser(logger);
				}
			}
		}

		public class when_parsing_an_inf_file_for_a_cf_installer : under.the_default_context
		{
			protected override void When()
			{
				inf = SUT.Parse(SampleInfContents.Sample);
			}

			[Test]
			public void it_should_find_the_SourceDisksNames_section()
			{
				inf.SourceDisksNames.ShouldNotBeNull();
			}

			[Test]
			public void it_should_find_the_SourceDisksFiles_section()
			{
				inf.SourceDisksFiles.ShouldNotBeNull();
			}

			[Test]
			public void it_should_find_the_files_sections()
			{
				inf.Files["Files"].ShouldNotBeNull();
			}

			[Test]
			public void it_should_find_the_common2_files_sections()
			{
				inf.Files["Files.Common2"].ShouldNotBeNull();
			}

			[Test]
			public void it_should_find_the_CEDevice_section()
			{
				inf.GetSection("CEDevice").ShouldNotBeNull();
			}

			[Test]
			public void it_should_not_find_a_non_existent_section()
			{
				inf.GetSection("this section name does not exist").ShouldBeNull();
			}
		}
	}
}
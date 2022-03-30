# Scripter & ScriptEngine Library
 
 This tool allows you to either use the console app to extract inserts, updates, deletes and selects from a database into a scripting file. I use this tool a lot to copy settings and other information from one database to another when I do not have access to the target database.
 The script will pull the actual values (based on the provided where clause) from the source database.
 
## Scripter tool
 
  The scripter tool supports a number of verbs and options to allow you to create these scripts right in the command line (for Windows, MAC, etc...). The following verbs and options are supported:
  
### inserts

 The inserts verb allows you to create insert statements from a set of records. the tool will also add an "if exists" check to the script for each record so that it will not cause any primary key conflicts.
 
 Available options:
 
  -x, --exists              checks if the record already exists

  -c, --connectionstring    connection string to database (you can use this or set the separate items with the info below)

  -s, --server              server name of the connection (instead of specifying connection string)

  -d, --database            database name of the connection (instead of specifying connection string)

  -i, --integrated          connection supports integrated security (instead of specifying connection string)

  -u, --user                user name for the connection (instead of specifying connection string)

  -p, --password            password for the connection (instead of specifying connection string)

  -t, --tablename           Database Table Name

  -k, --keys                List of primary keys of this table

  -w, --whereclause         where clause to filter output

  -o, --orderclause         order clause to sort output

  -l, --location            file location to write to

  --help                    Display this help screen.

  --version                 Display version information.

### updates

 The inserts verb allows you to create update statements from a set of records. the tool will also add an "if exists" check to the script for each record so that it will not cause any primary key conflicts.
 
 Available options:
  
  -x, --exists              checks if the record already exists

  -c, --connectionstring    connection string to database (you can use this or set the separate items with the info below)

  -s, --server              server name of the connection (instead of specifying connection string)

  -d, --database            database name of the connection (instead of specifying connection string)

  -i, --integrated          connection supports integrated security (instead of specifying connection string)

  -u, --user                user name for the connection (instead of specifying connection string)

  -p, --password            password for the connection (instead of specifying connection string)

  -t, --tablename           Database Table Name
  
  -f, --fields              list of fields to update (if none provided, all fields will be scripted for update)

  -k, --keys                List of primary keys of this table

  -w, --whereclause         where clause to filter output

  -o, --orderclause         order clause to sort output

  -l, --location            file location to write to

  --help                    Display this help screen.

  --version                 Display version information.
  
 ### deletes

 The inserts verb allows you to create delete statements from a set of records. the tool will also add an "if exists" check to the script for each record so that it will not cause any primary key conflicts.
 
 Available options:
 
  -x, --exists              checks if the record already exists

  -c, --connectionstring    connection string to database (you can use this or set the separate items with the info below)

  -s, --server              server name of the connection (instead of specifying connection string)

  -d, --database            database name of the connection (instead of specifying connection string)

  -i, --integrated          connection supports integrated security (instead of specifying connection string)

  -u, --user                user name for the connection (instead of specifying connection string)

  -p, --password            password for the connection (instead of specifying connection string)

  -t, --tablename           Database Table Name

  -k, --keys                List of primary keys of this table

  -w, --whereclause         where clause to filter output

  -o, --orderclause         order clause to sort output

  -l, --location            file location to write to

  --help                    Display this help screen.

  --version                 Display version information.

### selects

 The inserts verb allows you to create select statements from a set of records. the tool will also add an "if exists" check to the script for each record so that it will not cause any primary key conflicts. By default, the engine hard codes the values like <code>select 'xxx' as ColA ...</code> but you can also create a simple select statement by setting the --hardcode flag to false.
 
 Available options:
 
  -h, --hardcode            hard codes the select values (Default is true)

  -c, --connectionstring    connection string to database (you can use this or set the separate items with the info below)

  -s, --server              server name of the connection (instead of specifying connection string)

  -d, --database            database name of the connection (instead of specifying connection string)

  -i, --integrated          connection supports integrated security (instead of specifying connection string)

  -u, --user                user name for the connection (instead of specifying connection string)

  -p, --password            password for the connection (instead of specifying connection string)

  -t, --tablename           Database Table Name

  -k, --keys                List of primary keys of this table

  -w, --whereclause         where clause to filter output

  -o, --orderclause         order clause to sort output

  -l, --location            file location to write to

  --help                    Display this help screen.

  --version                 Display version information.
  
 ## Script Engine library
 
  You can use the script library to generate these statements in code if needed.
  
 # Usage
  
  Here's a sample command line that will extract a number of lines from a table in your database:
  
  '''
  scripter inserts --server "10.0.0.1" --database "VisionDemo" --user "sa" --password "supersecure" --tablename "PR" --keys "WBS1" "WBS2" "WBS3" --whereclause "PR.ChargeType = 'H' and PR.Status = 'A'" --location "c:\temp\myscript.sql"
  '''

 this is the generated info:
 
 '''
 if not exists (select 1 from PR where WBS1 = '0000001.00' AND WBS2 = ' ' AND WBS3 = ' ')
begin
insert into PR ([WBS1], [WBS2], [WBS3], [Name], [ChargeType], [SubLevel], [Principal], [ProjMgr], [Supervisor], [ClientID], [CLAddress], [Fee], [ReimbAllow], [ConsultFee], [BudOHRate], [Status], [RevType], [MultAmt], [Org], [UnitTable], [StartDate], [EndDate], [PctComp], [LabPctComp], [ExpPctComp], [BillByDefault], [BillableWarning], [Memo], [BudgetedFlag], [BudgetedLevels], [BillWBS1], [BillWBS2], [BillWBS3], [XCharge], [XChargeMethod], [XChargeMult], [Description], [Closed], [ReadOnly], [DefaultEffortDriven], [DefaultTaskType], [VersionID], [ContactID], [CLBillingAddr], [LongName], [Address1], [Address2], [Address3], [City], [State], [Zip], [County], [Country], [FederalInd], [ProjectType], [Responsibility], [Referable], [EstCompletionDate], [ActCompletionDate], [ContractDate], [BidDate], [ComplDateComment], [FirmCost], [FirmCostComment], [TotalProjectCost], [TotalCostComment], [OpportunityID], [ClientConfidential], [ClientAlias], [AvailableForCRM], [ReadyForApproval], [ReadyForProcessing], [BillingClientID], [BillingContactID], [Phone], [Fax], [EMail], [ProposalWBS1], [CreateUser], [CreateDate], [ModUser], [ModDate], [CostRateMeth], [CostRateTableNo], [PayRateMeth], [PayRateTableNo], [Locale], [LineItemApproval], [LineItemApprovalEK], [BudgetSource], [BudgetLevel], [ProfServicesComplDate], [ConstComplDate], [ProjectCurrencyCode], [BillingCurrencyCode], [RestrictChargeCompanies], [RevUpsetLimits], [RevUpsetWBS2], [RevUpsetWBS3], [RevUpsetIncludeComp], [RevUpsetIncludeCons], [RevUpsetIncludeReimb], [ProjectExchangeRate], [BillingExchangeRate], [FeeBillingCurrency], [ReimbAllowBillingCurrency], [ConsultFeeBillingCurrency], [PORMBRate], [POCNSRate], [PlanID], [TKCheckRPDate], [ICBillingLab], [ICBillingLabMethod], [ICBillingLabMult], [ICBillingExp], [ICBillingExpMethod], [ICBillingExpMult], [TKCheckRPPlannedHrs], [RequireComments], [BillByDefaultConsultants], [BillByDefaultOtherExp], [BillByDefaultORTable], [PhoneFormat], [FaxFormat], [RevType2], [RevType3], [RevType4], [RevType5], [RevUpsetCategoryToAdjust], [FeeFunctionalCurrency], [ReimbAllowFunctionalCurrency], [ConsultFeeFunctionalCurrency], [RevenueMethod], [ICBillingLabTableNo], [ICBillingExpTableNo], [Biller], [FeeDirLab], [FeeDirExp], [ReimbAllowExp], [ReimbAllowCons], [FeeDirLabBillingCurrency], [FeeDirExpBillingCurrency], [ReimbAllowExpBillingCurrency], [ReimbAllowConsBillingCurrency], [FeeDirLabFunctionalCurrency], [FeeDirExpFunctionalCurrency], [ReimbAllowExpFunctionalCurrency], [ReimbAllowConsFunctionalCurrency], [RevUpsetIncludeCompDirExp], [RevUpsetIncludeReimbCons], [AwardType], [Duration], [ContractTypeGovCon], [CompetitionType], [MasterContract], [Solicitation], [NAICS], [OurRole], [AjeraSync], [ServProCode], [FESurchargePct], [FESurcharge], [FEAddlExpensesPct], [FEAddlExpenses], [FEOtherPct], [FEOther], [ProjectTemplate], [AjeraSpentLabor], [AjeraSpentReimbursable], [AjeraSpentConsultant], [AjeraCostLabor], [AjeraCostReimbursable], [AjeraCostConsultant], [AjeraWIPLabor], [AjeraWIPReimbursable], [AjeraWIPConsultant], [AjeraBilledLabor], [AjeraBilledReimbursable], [AjeraBilledConsultant], [AjeraReceivedLabor], [AjeraReceivedReimbursable], [AjeraReceivedConsultant], [TLInternalKey], [TLProjectID], [TLProjectName], [TLChargeBandInternalKey], [TLChargeBandExternalCode], [TLSyncModDate], [PIMID]) values ('0000001.00', ' ', ' ', 'General Overhead', 'H', 'Y', '00001', NULL, NULL, NULL, '<Default>', 0.0000, 0.0000, 0.0000, 0.0000, 'A', 'B', 0.0000, 'CO:00', NULL, NULL, NULL, 0.0000, 0.0000, 0.0000, 'Y', 'N', NULL, 'N', NULL, NULL, NULL, NULL, 'G', 0, 0.0000, NULL, 0, 0, 0, 0, 0, NULL, NULL, 'General Overhead', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'N', NULL, NULL, 'N', NULL, NULL, NULL, NULL, NULL, 0.0000, NULL, 0.0000, NULL, NULL, 'N', NULL, 'N', 'Y', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, 'Admin', '2007-01-15T14:48:00.2600000', 'ADMIN', '2005-03-07T17:27:43.0000000', 0, 0, 0, 0, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, ' ', ' ', 'N', 'N', NULL, NULL, 'N', 'N', 'N', 0.0000000000, 0.0000000000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, NULL, 'N', 'G', 0, 0.0000, 'G', 0, 0.0000, 'N', 'C', 'E', 'E', 0, NULL, NULL, 'N', 'N', 'N', 'N', 0, 0.0000, 0.0000, 0.0000, 'U', 0, 0, NULL, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 'N', 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'N', NULL, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, '0', NULL, NULL, NULL, NULL, NULL, '0000001.00')
end
if not exists (select 1 from PR where WBS1 = '0000001.00' AND WBS2 = '000' AND WBS3 = ' ')
begin
insert into PR ([WBS1], [WBS2], [WBS3], [Name], [ChargeType], [SubLevel], [Principal], [ProjMgr], [Supervisor], [ClientID], [CLAddress], [Fee], [ReimbAllow], [ConsultFee], [BudOHRate], [Status], [RevType], [MultAmt], [Org], [UnitTable], [StartDate], [EndDate], [PctComp], [LabPctComp], [ExpPctComp], [BillByDefault], [BillableWarning], [Memo], [BudgetedFlag], [BudgetedLevels], [BillWBS1], [BillWBS2], [BillWBS3], [XCharge], [XChargeMethod], [XChargeMult], [Description], [Closed], [ReadOnly], [DefaultEffortDriven], [DefaultTaskType], [VersionID], [ContactID], [CLBillingAddr], [LongName], [Address1], [Address2], [Address3], [City], [State], [Zip], [County], [Country], [FederalInd], [ProjectType], [Responsibility], [Referable], [EstCompletionDate], [ActCompletionDate], [ContractDate], [BidDate], [ComplDateComment], [FirmCost], [FirmCostComment], [TotalProjectCost], [TotalCostComment], [OpportunityID], [ClientConfidential], [ClientAlias], [AvailableForCRM], [ReadyForApproval], [ReadyForProcessing], [BillingClientID], [BillingContactID], [Phone], [Fax], [EMail], [ProposalWBS1], [CreateUser], [CreateDate], [ModUser], [ModDate], [CostRateMeth], [CostRateTableNo], [PayRateMeth], [PayRateTableNo], [Locale], [LineItemApproval], [LineItemApprovalEK], [BudgetSource], [BudgetLevel], [ProfServicesComplDate], [ConstComplDate], [ProjectCurrencyCode], [BillingCurrencyCode], [RestrictChargeCompanies], [RevUpsetLimits], [RevUpsetWBS2], [RevUpsetWBS3], [RevUpsetIncludeComp], [RevUpsetIncludeCons], [RevUpsetIncludeReimb], [ProjectExchangeRate], [BillingExchangeRate], [FeeBillingCurrency], [ReimbAllowBillingCurrency], [ConsultFeeBillingCurrency], [PORMBRate], [POCNSRate], [PlanID], [TKCheckRPDate], [ICBillingLab], [ICBillingLabMethod], [ICBillingLabMult], [ICBillingExp], [ICBillingExpMethod], [ICBillingExpMult], [TKCheckRPPlannedHrs], [RequireComments], [BillByDefaultConsultants], [BillByDefaultOtherExp], [BillByDefaultORTable], [PhoneFormat], [FaxFormat], [RevType2], [RevType3], [RevType4], [RevType5], [RevUpsetCategoryToAdjust], [FeeFunctionalCurrency], [ReimbAllowFunctionalCurrency], [ConsultFeeFunctionalCurrency], [RevenueMethod], [ICBillingLabTableNo], [ICBillingExpTableNo], [Biller], [FeeDirLab], [FeeDirExp], [ReimbAllowExp], [ReimbAllowCons], [FeeDirLabBillingCurrency], [FeeDirExpBillingCurrency], [ReimbAllowExpBillingCurrency], [ReimbAllowConsBillingCurrency], [FeeDirLabFunctionalCurrency], [FeeDirExpFunctionalCurrency], [ReimbAllowExpFunctionalCurrency], [ReimbAllowConsFunctionalCurrency], [RevUpsetIncludeCompDirExp], [RevUpsetIncludeReimbCons], [AwardType], [Duration], [ContractTypeGovCon], [CompetitionType], [MasterContract], [Solicitation], [NAICS], [OurRole], [AjeraSync], [ServProCode], [FESurchargePct], [FESurcharge], [FEAddlExpensesPct], [FEAddlExpenses], [FEOtherPct], [FEOther], [ProjectTemplate], [AjeraSpentLabor], [AjeraSpentReimbursable], [AjeraSpentConsultant], [AjeraCostLabor], [AjeraCostReimbursable], [AjeraCostConsultant], [AjeraWIPLabor], [AjeraWIPReimbursable], [AjeraWIPConsultant], [AjeraBilledLabor], [AjeraBilledReimbursable], [AjeraBilledConsultant], [AjeraReceivedLabor], [AjeraReceivedReimbursable], [AjeraReceivedConsultant], [TLInternalKey], [TLProjectID], [TLProjectName], [TLChargeBandInternalKey], [TLChargeBandExternalCode], [TLSyncModDate], [PIMID]) values ('0000001.00', '000', ' ', 'Corporate Unassigned', 'H', 'N', NULL, NULL, NULL, NULL, '<Default>', 0.0000, 0.0000, 0.0000, 0.0000, 'A', 'B', 0.0000, 'CO:00', NULL, NULL, NULL, 0.0000, 0.0000, 0.0000, 'C', 'N', NULL, 'N', NULL, NULL, NULL, NULL, 'G', 0, 0.0000, NULL, 0, 0, 0, 0, 0, NULL, NULL, 'Corporate Unassigned', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'N', NULL, NULL, 'N', NULL, NULL, NULL, NULL, NULL, 0.0000, NULL, 0.0000, NULL, NULL, 'N', NULL, 'N', 'Y', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, 'Admin', '2007-01-15T14:48:00.2600000', 'Admin', '2005-01-15T14:48:00.2600000', 0, 0, 0, 0, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, ' ', ' ', 'N', 'N', NULL, NULL, 'N', 'N', 'N', 0.0000000000, 0.0000000000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, NULL, 'N', 'G', 0, 0.0000, 'G', 0, 0.0000, 'N', 'C', 'E', 'E', 0, NULL, NULL, 'N', 'N', 'N', 'N', 0, 0.0000, 0.0000, 0.0000, 'U', 0, 0, NULL, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 'N', 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'N', NULL, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, '0', NULL, NULL, NULL, NULL, NULL, '0000001.00')
end
if not exists (select 1 from PR where WBS1 = '0000001.00' AND WBS2 = '100' AND WBS3 = ' ')
begin
insert into PR ([WBS1], [WBS2], [WBS3], [Name], [ChargeType], [SubLevel], [Principal], [ProjMgr], [Supervisor], [ClientID], [CLAddress], [Fee], [ReimbAllow], [ConsultFee], [BudOHRate], [Status], [RevType], [MultAmt], [Org], [UnitTable], [StartDate], [EndDate], [PctComp], [LabPctComp], [ExpPctComp], [BillByDefault], [BillableWarning], [Memo], [BudgetedFlag], [BudgetedLevels], [BillWBS1], [BillWBS2], [BillWBS3], [XCharge], [XChargeMethod], [XChargeMult], [Description], [Closed], [ReadOnly], [DefaultEffortDriven], [DefaultTaskType], [VersionID], [ContactID], [CLBillingAddr], [LongName], [Address1], [Address2], [Address3], [City], [State], [Zip], [County], [Country], [FederalInd], [ProjectType], [Responsibility], [Referable], [EstCompletionDate], [ActCompletionDate], [ContractDate], [BidDate], [ComplDateComment], [FirmCost], [FirmCostComment], [TotalProjectCost], [TotalCostComment], [OpportunityID], [ClientConfidential], [ClientAlias], [AvailableForCRM], [ReadyForApproval], [ReadyForProcessing], [BillingClientID], [BillingContactID], [Phone], [Fax], [EMail], [ProposalWBS1], [CreateUser], [CreateDate], [ModUser], [ModDate], [CostRateMeth], [CostRateTableNo], [PayRateMeth], [PayRateTableNo], [Locale], [LineItemApproval], [LineItemApprovalEK], [BudgetSource], [BudgetLevel], [ProfServicesComplDate], [ConstComplDate], [ProjectCurrencyCode], [BillingCurrencyCode], [RestrictChargeCompanies], [RevUpsetLimits], [RevUpsetWBS2], [RevUpsetWBS3], [RevUpsetIncludeComp], [RevUpsetIncludeCons], [RevUpsetIncludeReimb], [ProjectExchangeRate], [BillingExchangeRate], [FeeBillingCurrency], [ReimbAllowBillingCurrency], [ConsultFeeBillingCurrency], [PORMBRate], [POCNSRate], [PlanID], [TKCheckRPDate], [ICBillingLab], [ICBillingLabMethod], [ICBillingLabMult], [ICBillingExp], [ICBillingExpMethod], [ICBillingExpMult], [TKCheckRPPlannedHrs], [RequireComments], [BillByDefaultConsultants], [BillByDefaultOtherExp], [BillByDefaultORTable], [PhoneFormat], [FaxFormat], [RevType2], [RevType3], [RevType4], [RevType5], [RevUpsetCategoryToAdjust], [FeeFunctionalCurrency], [ReimbAllowFunctionalCurrency], [ConsultFeeFunctionalCurrency], [RevenueMethod], [ICBillingLabTableNo], [ICBillingExpTableNo], [Biller], [FeeDirLab], [FeeDirExp], [ReimbAllowExp], [ReimbAllowCons], [FeeDirLabBillingCurrency], [FeeDirExpBillingCurrency], [ReimbAllowExpBillingCurrency], [ReimbAllowConsBillingCurrency], [FeeDirLabFunctionalCurrency], [FeeDirExpFunctionalCurrency], [ReimbAllowExpFunctionalCurrency], [ReimbAllowConsFunctionalCurrency], [RevUpsetIncludeCompDirExp], [RevUpsetIncludeReimbCons], [AwardType], [Duration], [ContractTypeGovCon], [CompetitionType], [MasterContract], [Solicitation], [NAICS], [OurRole], [AjeraSync], [ServProCode], [FESurchargePct], [FESurcharge], [FEAddlExpensesPct], [FEAddlExpenses], [FEOtherPct], [FEOther], [ProjectTemplate], [AjeraSpentLabor], [AjeraSpentReimbursable], [AjeraSpentConsultant], [AjeraCostLabor], [AjeraCostReimbursable], [AjeraCostConsultant], [AjeraWIPLabor], [AjeraWIPReimbursable], [AjeraWIPConsultant], [AjeraBilledLabor], [AjeraBilledReimbursable], [AjeraBilledConsultant], [AjeraReceivedLabor], [AjeraReceivedReimbursable], [AjeraReceivedConsultant], [TLInternalKey], [TLProjectID], [TLProjectName], [TLChargeBandInternalKey], [TLChargeBandExternalCode], [TLSyncModDate], [PIMID]) values ('0000001.00', '100', ' ', 'Boston Unassigned', 'H', 'N', NULL, NULL, NULL, NULL, '<Default>', 0.0000, 0.0000, 0.0000, 0.0000, 'A', 'B', 0.0000, 'BO:AD', NULL, NULL, NULL, 0.0000, 0.0000, 0.0000, 'C', 'N', NULL, 'N', NULL, NULL, NULL, NULL, 'G', 0, 0.0000, NULL, 0, 0, 0, 0, 0, NULL, NULL, 'Boston Unassigned', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'N', NULL, NULL, 'N', NULL, NULL, NULL, NULL, NULL, 0.0000, NULL, 0.0000, NULL, NULL, 'N', NULL, 'N', 'Y', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, 'Admin', '2007-01-15T14:48:00.2600000', 'Admin', '2005-01-15T14:48:00.2600000', 0, 0, 0, 0, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, ' ', ' ', 'N', 'N', NULL, NULL, 'N', 'N', 'N', 0.0000000000, 0.0000000000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, NULL, 'N', 'G', 0, 0.0000, 'G', 0, 0.0000, 'N', 'C', 'E', 'E', 0, NULL, NULL, 'N', 'N', 'N', 'N', 0, 0.0000, 0.0000, 0.0000, 'U', 0, 0, NULL, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 'N', 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'N', NULL, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, NULL, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, '0', NULL, NULL, NULL, NULL, NULL, '0000001.00')
end
 '''
